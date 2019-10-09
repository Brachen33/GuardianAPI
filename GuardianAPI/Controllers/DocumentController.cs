using GuardianAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;



namespace GuardianAPI.Controllers
{
    [Route("api/[controller]")]
    public class DocumentController : Controller
    {
        private readonly IDocumentRepository _documentRepository;


        public DocumentController(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        //[Route("GetLatestPhoto/{id}")]
        //public IActionResult GetParticipantLatestPhotoByParticipantId(int id)
        //{
        //    var img = _documentRepository.GetLatestParticipantPhotoByParticipantId(id);
        //    return RedirectToPage("/Views/SeePhoto.cshtml");             
        //}

    }
}
