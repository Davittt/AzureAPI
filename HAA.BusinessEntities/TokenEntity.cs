
namespace HAA.BusinessEntities
{
	using System;
    using System.Collections.Generic;

    public partial class TokenEntity
	{
        public int Id { get; set; }
        public int UserId { get; set; }
        public string AuthToken { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
