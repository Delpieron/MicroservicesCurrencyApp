namespace DBModels.Models;

public class Currency
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Symbol { get; set; }
    public double Price { get; set; }

    public ICollection<User>? Users { get; set; }
} 