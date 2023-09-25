using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi;

public class Book
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
    public int Id { get; set; }

    public string Title { get; set; }  

    public int GenreId { get; set; } //Id'yi entityden alacak 

    public int TotalPages { get; set; }  

    public DateTime PublishDate { get; set; } 
}
