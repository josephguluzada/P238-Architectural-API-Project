namespace BlankSolution.Core.Entities;

public class MovieImage : BaseEntity
{
    public int MovieId { get; set; }
    public string ImageUrl { get; set; }

    public Movie Movie { get; set; }
}
