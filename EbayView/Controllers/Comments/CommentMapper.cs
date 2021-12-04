namespace EbayView.Controllers.Comments
{
    using AutoMapper;
    using EbayView.Models.ViewModel.Category;
    using EbayView.Models.ViewModel.Comments;
    using global::Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CommentMapper : Profile
    {
        public CommentMapper()
        {
            CreateMap<GetCommentOutputModel, Comment>();

        }
    }
}
