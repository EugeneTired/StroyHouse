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
    
    public partial class ProWare
    {
        public int ID { get; set; }
        public Nullable<int> IDprod { get; set; }
        public Nullable<int> IDware { get; set; }
        public int AmountWare { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual WareHouse WareHouse { get; set; }
    }
}
