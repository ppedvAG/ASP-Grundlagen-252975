using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace M000_DataAccess.Models;

public partial class KursInhalte
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("KursID")]
    public int? KursId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string InhaltTitel { get; set; }

    [ForeignKey("KursId")]
    [InverseProperty("KursInhalte")]
    public virtual Kurse Kurs { get; set; }
}