//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PlayerWPF2
{
    using System;
    using System.Collections.Generic;
    
    public partial class Player
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public Nullable<int> Games { get; set; }
        public Nullable<int> Goals { get; set; }
        public Nullable<int> Assists { get; set; }
        public Nullable<int> CleanSheets { get; set; }
        public Nullable<int> PlayStyleId { get; set; }
        public Nullable<int> AgentId { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<int> Salary { get; set; }
        public Nullable<int> PlayerPrice { get; set; }
        public Nullable<bool> IsInjured { get; set; }
    
        public virtual Agent Agent { get; set; }
        public virtual PlayStyle PlayStyle { get; set; }
    }
}
