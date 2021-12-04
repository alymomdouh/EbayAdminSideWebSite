namespace EbayView.Controllers.Comments
{
    using AutoMapper;
    using EbayView.Models.ViewModel.Comments;
    using global::Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class CommentController : Controller
    {
        private readonly ICommentRepository _CommentRepository;
        private readonly IMapper _mapper;
        public CommentController(ICommentRepository CommentRepository, IMapper mapper)
        {
            _CommentRepository = CommentRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Comments = await _CommentRepository.GetCommentAsync();

            var result = _mapper.Map<List<GetCommentOutputModel>>(Comments);

            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var Comments = await _CommentRepository.GetCommentDetailsAsync(id);
           
            return View(Comments);
        }
        //[HttpGet]
        //public ActionResult Create()
        //{
        //    return View();
        //}




        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([FromBody] GetCommentOutputModel model)
        //{
        //    try
        //    {
        //        var Comment = _mapper.Map<Comment>(model);
        //        await _CommentRepository.AddCommentAsync(Comment);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}





    }
}
