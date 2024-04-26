namespace BlankSolution.Core.Entities;

public class Movie : BaseEntity
	{
    public int GenreId { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public double CostPrice { get; set; }
    public double SalePrice { get; set; }

    public List<MovieImage> MovieImages { get; set; }
    public Genre Genre { get; set; }
}
