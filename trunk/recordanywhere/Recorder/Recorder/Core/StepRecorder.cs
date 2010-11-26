using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Recorder.Core
{
    /// <summary>
    /// use to record the step to source grid
    /// </summary>
    public class StepRecorder
    {
        public static void RecordStep(SourceGrid.Grid grid, string step)
        {
            int rows = grid.RowsCount;
            grid.Rows.Insert(rows);
            grid[rows, 0] = new SourceGrid.Cells.Cell(step);
        }

        public static void RecordStep(TreeView tree, string step)
        {
            if (tree.Nodes[0] != null)
            {
                tree.BeginUpdate();
                tree.Nodes[0].Nodes.Insert(tree.Nodes[0].Nodes.Count, step);
                tree.EndUpdate();
                tree.ExpandAll();
            }
        }
    }
}
