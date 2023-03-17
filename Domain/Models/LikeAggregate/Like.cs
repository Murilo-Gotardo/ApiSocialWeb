﻿using apiSocialWeb.Domain.Models.PostsAggregate;
using apiSocialWeb.Domain.Models.UserAggregate;
using System.ComponentModel.DataAnnotations;

namespace apiSocialWeb.Domain.Models.LikeAggregate
{
    public class Like
    {

        [Key]
        public int LikeId { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int PostId { get; set; }

        public Posts Post { get; set; }


        public Like()
        {
        }

        public Like(int postId, int userId)
        {
            UserId = userId;
            PostId = postId;
        }
    }
}
