using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Chess.Models;
using Chess.Service;
using Microsoft.AspNetCore.Mvc;

namespace Chess.Controllers
{
    [Route("api/[controller]")]
    public class ChessController : Controller
    {
        private ChessService _chessService;

        public ChessController(ChessService service)
        {
            this._chessService = service;
        }

        // GET: api/chess
        // Used for testing 
        [HttpGet]
        public string Get()
        {
            string boardString = "[[\"HR\", \"HKn\", \"HB\", \"HK\", \"HQ\", \"HB\", \"HKn\", \"HR\"], [\"HP\", \"HP\", \"HP\", \"HP\", \"HP\", \"HP\", \"HP\", \"HP\"], [],[],[],[],[\"BP\", \"BP\", \"BP\", \"BP\", \"BP\", \"BP\", \"BP\", \"BP\"],[\"BR\", \"BKn\", \"BB\", \"BQ\", \"BK\", \"BB\", \"BKn\", \"BR\"]]";
            return boardString;
        }


        // POST api/chess
        [HttpPost]
        public string Post([FromBody] JsonElement element)
        {
            string tmp = element.ToString();
            string res = this._chessService.makeMove(element);
            return res;
        }
    }
}
