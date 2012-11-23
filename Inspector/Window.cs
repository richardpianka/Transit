using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using ProtoBuf;
using Transit.Analysis;
using Transit.Common.Extensions;
using Transit.Common.Model;
using Transit.Common.Utilities;
using Point = Transit.Common.Model.Point;

namespace Transit.Inspector
{
    public partial class Window : Form
    {
        private List<Match> _matches;
        private Dictionary<string, Match> _matchMap;
        private MatchCollection _matchCollection;
        private readonly StartupParameters _startupParameters;

        public Window(StartupParameters startupParameters)
        {
            InitializeComponent();
            _startupParameters = startupParameters;
        }

        private string MatchToKey(Match match)
        {
            return string.Join("|", match.Device, match.Shape.Id, match.StartTime.ToString("MM/dd/yyyy HH:mm:ss"));
        }

        private string RowToKey(DataGridViewRow row)
        {
            return string.Join("|", row.Cells[1].Value, row.Cells[2].Value, row.Cells[3].Value + " " + row.Cells[4].Value);
        }

        private void WindowLoad(object sender, EventArgs e)
        {
            SetupMap();
            if (_startupParameters.FileProvided)
            {
                LockAndLoad(_startupParameters.File);
            }
        }

        private void LockAndLoad(string file)
        {
            ToggleEnabled(false);
            statusLabel.Text = @"Loading...";
            ThreadPool.QueueUserWorkItem(x => InitializeAll(file));
        }

        private void ToggleEnabled(bool enabled)
        {
            Controls.OfType<Control>()
                    .ToList()
                    .Where(x => x.GetType() != typeof(MenuStrip))
                    .ForEach(x => x.Enabled = enabled);

            openToolStripMenuItem.Enabled = enabled;
            saveToolStripMenuItem.Enabled = enabled;
        }

        private void InitializeAll(string file)
        {
            _matches = Data.LoadMatches(file).ToList();
            _matchMap = _matches.ToDictionary(MatchToKey);
            _matchCollection = new MatchCollection(_matches);
            RenderMatches();
        }

        private void SetupMap()
        {
            Invoke((Action) delegate
                                {
                                    GMapProvider.WebProxy = WebRequest.GetSystemWebProxy();
                                    GMapProvider.WebProxy.Credentials = CredentialCache.DefaultCredentials;

                                    map.MapProvider = GMapProviders.BingMap;
                                    map.MinZoom = 3;
                                    map.MaxZoom = 17;
                                    map.Zoom = 4;
                                    map.Manager.Mode = AccessMode.ServerAndCache;
                                    map.SetCurrentPositionByKeywords("California");

                                    zoomBar.Minimum = map.MinZoom;
                                    zoomBar.Maximum = map.MaxZoom;
                                    zoomBar.TickFrequency = 1;
                                    zoomBar.Value = Convert.ToInt32(map.Zoom);
                                });
        }

        private void RenderMatches()
        {
            DataTable table = new DataTable().AddColumn(" ", typeof(bool))
                                             .AddReadonlyColumn("Device")
                                             .AddReadonlyColumn("Shape")
                                             .AddReadonlyColumn("Date")
                                             .AddReadonlyColumn("Start")
                                             .AddReadonlyColumn("End")
                                             .AddReadonlyColumn("Total");

            foreach (Match match in _matches)
            {
                table.Rows.Add(new object[]
                                   {
                                       true,
                                       match.Device,
                                       match.Shape.Id,
                                       match.StartTime.ToString("MM/dd/yyy"),
                                       match.StartTime.ToString("HH:mm:ss"),
                                       match.EndTime.ToString("HH:mm:ss"),
                                       match.Duration,
                                   });
            }

            Invoke((Action) delegate
                                {
                                    captureGrid.DataSource = table;
                                    captureGrid.Columns[0].Width = 30;
                                    statusLabel.Text = @"Finished loading";
                                    ToggleEnabled(true);
                                });
        }

        private void ApplyFilter()
        {
            try
            {
                string showExcludedRowsFilter = showExcludedRows.Checked ? string.Empty : "[ ] = true AND ";
                ((DataTable)captureGrid.DataSource).DefaultView.RowFilter = showExcludedRowsFilter + filterBox.Text.IfEmpty("true");
            // ReSharper disable EmptyGeneralCatchClause
            } catch (Exception) { }
            // ReSharper enable EmptyGeneralCatchClause

            captureGrid.Refresh();
        }

        private void FilterHelpLinkLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.csharp-examples.net/dataview-rowfilter/");
        }

        private void CaptureGridSelectionChanged(object sender, EventArgs e)
        {
            if (captureGrid.SelectedRows.Count != 1) return;

            DataTable table = new DataTable().AddColumn(" ", typeof (bool))
                .AddReadonlyColumn("Stop")
                .AddReadonlyColumn("Time")
                .AddReadonlyColumn("Distance");

            Match match = _matchMap[RowToKey(captureGrid.SelectedRows[0])];
            List<Point> capturedPoints = new List<Point>();
            foreach (OrderedStop stop in match.Shape.Stops)
            {
                foreach (MatchRead read in match.Points[stop])
                {
                    capturedPoints.Add(read.Read.Point);
                    table.Rows.Add(new object[]
                                       {
                                           read.Included,
                                           read.Read.ClosestStop,
                                           read.Read.Date.ToString("HH:mm:ss"),
                                           Math.Round(read.Read.Distance, 1) + " ft"
                                       });
                }
            }

            readsGrid.DataSource = table;
            readsGrid.Columns[0].Width = 30;
            readsGrid.Columns[1].Width = 180;

            map.Overlays.Clear();
            MapUtilities.DrawMap(map, capturedPoints, Color.Red, false);
            MapUtilities.DrawMap(map, match.Shape.Stops.OrderBy(x => x.Order).Select(x => x.Point).ToList(), Color.DarkBlue, true);

            zoomBar.Value = Convert.ToInt32(map.Zoom);
        }

        private void ReadsGridCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (captureGrid.SelectedRows.Count < 1) return;

            foreach (DataGridViewRow row in captureGrid.SelectedRows)
            {
                Match match = _matchMap[RowToKey(row)];
                bool included = (bool)readsGrid.Rows[e.RowIndex].Cells[0].Value;

                match.Points.Keys.SelectMany(x => match.Points[x]).ToList()[e.RowIndex].Included = included;
            }
        }

        private void CaptureGridKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                ToggleSelected();
            }
        }

        private void ToggleSelected()
        {
            try
            {
                captureGrid.CellValueChanged -= CaptureGridCellValueChanged;
                // toggles all selected rows to the opposite of the first selected row
                bool selected = !(bool)captureGrid.SelectedRows[0].Cells[0].Value;

                foreach (DataGridViewRow row in captureGrid.SelectedRows)
                {
                    row.Cells[0].Value = selected;
                    _matchCollection.SetInclusion(_matchMap[RowToKey(row)], !selected);
                }
            }
            finally
            {
                captureGrid.CellValueChanged += CaptureGridCellValueChanged;
            }
        }

        private void FilterBoxKeyUp(object sender, KeyEventArgs e)
        {
            ApplyFilter();
        }

        private void ShowExcludedRowsCheckedChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void CaptureGridCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ToggleSelected();
//            DataGridViewRow row = captureGrid.Rows[e.RowIndex];
//            Match match = _matchMap[RowToKey(row)];
//            bool isChecked = (bool)row.Cells[e.ColumnIndex].Value;
//            _matchCollection.SetInclusion(match, isChecked);

            ApplyFilter();
        }

        private void OpenToolStripMenuItemClick(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
                                                {
                                                    Filter = @"Match collections (*.transit)|*.transit|All files (*.*)|*.*",
                                                    FilterIndex = 1
                                                };

            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                LockAndLoad(openFileDialog.FileName);
            }
        }

        private void SaveToolStripMenuItemClick(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
                                                {
                                                    Filter = @"Match collections (*.transit)|*.transit|All files (*.*)|*.*",
                                                    FilterIndex = 1
                                                };

            DialogResult result = saveFileDialog.ShowDialog();
            if (result != DialogResult.OK) return;

            File.Delete(saveFileDialog.FileName);

            using (Stream writer = File.OpenWrite(saveFileDialog.FileName))
            {
                Serializer.Serialize(writer, _matchCollection.IncludedMatches.ToList());
            }
        }

        private void CaptureGridCellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex > -1)
            {
                captureGrid.EndEdit();
            }
        }

        private void MapDoubleClick(object sender, EventArgs e)
        {
            map.Zoom = Math.Min(map.Zoom + 1, map.MaxZoom);
        }

        private void ZoomBarScroll(object sender, EventArgs e)
        {
            map.Zoom = zoomBar.Value;
        }

        private void captureGrid_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0 && captureGrid.SelectedRows.OfType<DataGridViewRow>().Any(x => x.Index == e.RowIndex))
            {
                ToggleSelected();
            }
        }
    }
}
