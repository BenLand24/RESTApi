using System;
using System.Collections.Generic;

public class Battery
{
    public Battery()
    {
        Columns = new HashSet<Column>();
    }

    public long id { get; set; }
    public string battery_type { get; set; }
    public string status { get; set; }
    public DateTime date_commision { get; set; }
    public DateTime date_last_inspect { get; set; }
    public string certificate_operations { get; set; }
    public string info { get; set; }
    public string notes { get; set; }
    public long? building_id { get; set; }
    public long? employee_id { get; set; }

    public virtual Building Building { get; set; }
    public virtual ICollection<Column> Columns { get; set; }
}
