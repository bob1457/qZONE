namespace quZONE.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserProfile //: BaseEntity<int>
    {
        public int Id { get; set; }
        

        public int OrgainzationId { get; set; }

        public int PositionId { get; set; }

        public string AvatarImgUrl { get; set; }

        public string AvatarImgUrlSm { get; set; }

        public string AvatarImgUrlMd { get; set; }

        public string ContactEmail { get; set; }


        //public UserProfile DisplayProfile(int id)
        //{
            
        //}
    }
}
