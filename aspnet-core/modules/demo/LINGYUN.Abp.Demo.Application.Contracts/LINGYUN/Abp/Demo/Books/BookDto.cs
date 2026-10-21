using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace LINGYUN.Abp.Demo.Books;
public class BookDto : AuditedEntityDto<Guid>
{
    [DisplayName("name")]
    public string Name { get; set; } = default!;

    [DisplayName("Type")]
    public BookType Type { get; set; }

    [DisplayName("Publication Date")]
    public DateTime PublishDate { get; set; }

    [DisplayName("Price")]
    public float Price { get; set; }

    public Guid AuthorId { get; set; }

    [DisplayName("Author")]
    public string AuthorName { get; set; } = default!;
}
