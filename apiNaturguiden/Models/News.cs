//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace apiNaturguiden.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class News
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public News()
        {
            this.NewsPicture = new HashSet<NewsPicture>();
        }
    
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public string Creator { get; set; }
        public string Heading { get; set; }
        public string Text { get; set; }
        public Nullable<int> Picture { get; set; }
        public string LinkUrl { get; set; }
        public string LinkText { get; set; }
        public int Position { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NewsPicture> NewsPicture { get; set; }
    }
}
