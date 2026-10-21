using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LINGYUN.Abp.Demo.Books;
public class BookImportDto
{
    [Required]
    [StringLength(128)]
    [DisplayName("name")]
    public string Name { get; set; } = default!;

    [Required]
    [DisplayName("Type")]
    public BookType Type { get; set; } = BookType.Undefined;

    [Required]
    [DataType(DataType.Date)]
    [DisplayName("Publication Date")]
    public DateTime PublishDate { get; set; } = DateTime.Now;

    [Required]
    [DisplayName("Price")]
    public float Price { get; set; }

    public Guid AuthorId { get; set; }

    [DisplayName("Author")]
    public string AuthorName { get; set; } = default!;
}
