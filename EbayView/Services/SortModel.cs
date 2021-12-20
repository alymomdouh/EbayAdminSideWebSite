using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbayView.Services
{
    //public enum SortOrder { Ascending = 0, Descending = 1 }
    public class SortModel
    {
        private string UpIcon = "fa fa-arrow-up";
        private string DownIcon = "fa fa-arrow-down";
        public string sortproperty { get; set; }
        public SortOrder sortOrder { get; set; }
        private List<SortTableColumn> sortTableColumns = new List<SortTableColumn>();

        public void AddColumn(string colname, bool IsDefaultColumn = false)
        {
            //SortTableColumn tem = this.sortTableColumns.Where(c => c.ColumnName.ToLower() == colname.ToLower()).SingleOrDefault();
            SortTableColumn tem = this.sortTableColumns.Where(c => c.ColumnName == colname).SingleOrDefault();
            if (tem == null)
            {
                sortTableColumns.Add(new SortTableColumn() { ColumnName = colname });
            }
            if (IsDefaultColumn == true || sortTableColumns.Count == 1)
            {
                sortproperty = colname;
                sortOrder = SortOrder.Ascending;
            }
        }
        public SortTableColumn GetColumn(string colname)
        {
            //SortTableColumn tem = this.sortTableColumns.Where(c => c.ColumnName.ToLower() == colname.ToLower()).SingleOrDefault();
            SortTableColumn tem = this.sortTableColumns.Where(c => c.ColumnName == colname).SingleOrDefault();
            if (tem == null)
            {
                sortTableColumns.Add(new SortTableColumn() { ColumnName = colname });
            }
            return tem;
        }
        public void applySort(string sortExpression)
        { 
            if (sortExpression == "") { sortExpression = this.sortproperty; }
            //sortExpression = sortExpression.ToLower();
            foreach (SortTableColumn sortTableColumn in this.sortTableColumns)
            {
                sortTableColumn.SortIcon = "";
                sortTableColumn.SortExpression = sortTableColumn.ColumnName;
                if (sortExpression == sortTableColumn.ColumnName)
                {
                    sortOrder = SortOrder.Ascending;
                    sortproperty = sortTableColumn.ColumnName;
                    sortTableColumn.SortIcon = DownIcon;
                    sortTableColumn.SortExpression = sortTableColumn.ColumnName + "_desc";
                }
                if (sortExpression == sortTableColumn.ColumnName + "_desc")
                {
                    this.sortOrder = SortOrder.Descending;
                    this.sortproperty = sortTableColumn.ColumnName;
                    sortTableColumn.SortIcon = UpIcon;
                    sortTableColumn.SortExpression = sortTableColumn.ColumnName;
                }
            } 
        }
    }
    public class SortTableColumn
    {
        public string ColumnName { get; set; }
        public string SortExpression { get; set; }
        public string SortIcon { get; set; }
    }
}
