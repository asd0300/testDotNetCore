using System;
using System.Collections.Generic;

namespace tododotnet6prac.Models;

public partial class JobTitle
{
    public Guid JobTitleId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}
