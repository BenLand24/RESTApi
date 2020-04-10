using System;

public class Elevator
{
    public long id { get; set; }
    public long serial_number { get; set; }
    public string model { get; set; }
    public string elevator_type { get; set; }
    public string status { get; set; }
    public DateTime date_commision { get; set; }
    public DateTime date_last_inspect { get; set; }
    public string certificate_inspect { get; set; }
    public string info { get; set; }
    public string notes { get; set; }
    public long? column_id { get; set; }
    
    public virtual Column Column { get; set; }
}