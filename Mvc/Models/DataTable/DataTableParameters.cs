namespace Mvc.Models.DataTable;

public class JQueryDataTableParams
{
    public int Draw { get; set; }
    public int Start { get; set; }
    public int Length { get; set; }


    public JQueryDataTableColumnSearch Search { get; set; }
    public List<JQueryDataTableColumnOrder> Order { get; set; }
    public List<JQueryDataTableColumn> Columns { get; set; }
}


public enum JQueryDataTableColumnOrderDirection
{
    Asc, Desc
}

public class JQueryDataTableColumnOrder
{
    public int Column { get; set; }
    public JQueryDataTableColumnOrderDirection Dir { get; set; }
}
public class JQueryDataTableColumnSearch
{
    public string Value { get; set; }
    public string Regex { get; set; }
}

public class JQueryDataTableColumn
{
    public string Data { get; set; }
    public string Name { get; set; }
    public Boolean Searchable { get; set; }
    public Boolean Orderable { get; set; }
    public JQueryDataTableColumnSearch Search { get; set; }
}