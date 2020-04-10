using System;
using System.Collections.Generic;

public class Column
{
    public Column()
    {
        Elevators = new HashSet<Elevator>();
    }

    public long id { get; set; }
    public string column_type { get; set; }
    public int number_floors { get; set; }
    public string status { get; set; }
    public string info { get; set; }
    public string notes { get; set; }
    public long? battery_id { get; set; }

    public virtual Battery Battery { get; set; }
    public virtual ICollection<Elevator> Elevators { get; set; }
}