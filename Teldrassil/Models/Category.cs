using System;
namespace Teldrassil.Models;

public class Category
{
    public int Id { set; get; }
    public string? Name { set; get; }
    public int ParentId { set; get; }
    public bool Visible { set; get; } = true;

}

