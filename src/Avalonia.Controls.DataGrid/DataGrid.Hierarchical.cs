// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.

namespace Avalonia.Controls
{
    /// <summary>
    /// Hierarchical helpers for DataGrid.
    /// </summary>
    partial class DataGrid
    {
        internal bool TryToggleHierarchicalAtSlot(int slot)
        {
            if (!_hierarchicalRowsEnabled || _hierarchicalAdapter == null)
            {
                return false;
            }

            if (slot < 0 || slot >= _hierarchicalAdapter.Count)
            {
                return false;
            }

            // Keep grouping separate unless explicitly configured to treat groups as nodes.
            if (RowGroupHeadersTable != null && RowGroupHeadersTable.Contains(slot))
            {
                if (_hierarchicalModel?.Options.TreatGroupsAsNodes == true)
                {
                    // Future integration path: groups-as-nodes could be toggled here.
                    return false;
                }

                return false;
            }

            _hierarchicalAdapter.Toggle(slot);
            return true;
        }
    }
}
