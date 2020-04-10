using System;
using System.Collections.Generic;

public class Building
{
    public Building()
    {
        Batteries = new HashSet<Battery>();
    }

    public long id { get; set; }
    public string admin_full_name { get; set; }
    public string admin_email { get; set; }
    public string admin_phone { get; set; }
    public string tech_full_name { get; set; }
    public string tech_email { get; set; }
    public string tech_phone { get; set; }

    public virtual ICollection<Battery> Batteries { get; set; }
}