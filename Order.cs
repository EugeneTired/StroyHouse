//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ВПФки
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.ProOrd = new HashSet<ProOrd>();
        }
    
        public int IDord { get; set; }
        public Nullable<int> IDuser { get; set; }
        public Nullable<int> IDship { get; set; }
        public System.DateTime SaleDate { get; set; }
        public System.DateTime ShipDate { get; set; }
        public bool Status { get; set; }
    
        public virtual Shipment Shipment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProOrd> ProOrd { get; set; }
        public virtual User User { get; set; }
    }
}
