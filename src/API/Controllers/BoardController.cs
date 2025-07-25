﻿using Application.DTO;
using Application.DTO.Board;
using Application.Interfaces.API;
using Asp.Versioning;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/board")]
    public class BoardController : BaseController<IBoardService>
    {
        private readonly IBoardService _service;
        public BoardController(IBoardService service) : base(service)
        {
            _service = service;
        }

        /// <summary>
        /// Добавить Board
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">Ответ при успешном запросе</response>
        /// <response code="400">Невалидные входные данные</response>
        /// <response code="401">Не авторизован</response>
        /// <response code="403">доступ к запрашиваемому ресурсу запрещен</response>
        /// <response code="422">Необрабатываемый объект</response>
        /// <response code="500">Ошибка сервера</response>
        /// <response code="501">Не реализовано</response>
        [HttpPost]
        [Authorize(Roles = RoleConst.Admin)]
        [ProducesResponseType(typeof(BoardResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateBoardRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var res = await _service.CreateAsync(request, long.Parse(userId));
            return Ok(res);
        }


        /// <summary>
        /// Получить Board
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">Ответ при успешном запросе</response>
        /// <response code="400">Невалидные входные данные</response>
        /// <response code="401">Не авторизован</response>
        /// <response code="403">доступ к запрашиваемому ресурсу запрещен</response>
        /// <response code="422">Необрабатываемый объект</response>
        /// <response code="500">Ошибка сервера</response>
        /// <response code="501">Не реализовано</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<BoardResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] FilteringBoardRequest request)
        {
            var res = await _service.GetAsync(request);
            return Ok(res);
        }

        /// <summary>
        /// Ручка с TotalCount для фронта
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("with-count")]
        [ProducesResponseType(typeof(WithCountResponse<BoardResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWithCount([FromQuery] FilteringBoardRequest request)
        {
            var res = await _service.GetWithCountAsync(request);
            return Ok(res);
        }

        /// <summary>
        /// Получить Board по id
        /// </summary>
        /// <param name="obj_id"></param>
        /// <response code="200">Ответ при успешном запросе</response>
        /// <response code="400">Невалидные входные данные</response>
        /// <response code="401">Не авторизован</response>
        /// <response code="403">доступ к запрашиваемому ресурсу запрещен</response>
        /// <response code="422">Необрабатываемый объект</response>
        /// <response code="500">Ошибка сервера</response>
        /// <response code="501">Не реализовано</response>
        [HttpGet("{obj_id}")]
        [ProducesResponseType(typeof(BoardResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(long obj_id)
        {
            var res = await _service.GetOneAsync(obj_id);
            return Ok(res);
        }

        /// <summary>
        /// Передвижения платы по шагам процесса
        /// </summary>
        /// <param name="obj_id"></param>
        /// <response code="200">Ответ при успешном запросе</response>
        /// <response code="400">Невалидные входные данные</response>
        /// <response code="401">Не авторизован</response>
        /// <response code="403">доступ к запрашиваемому ресурсу запрещен</response>
        /// <response code="422">Необрабатываемый объект</response>
        /// <response code="500">Ошибка сервера</response>
        /// <response code="501">Не реализовано</response>
        [HttpPost("move")]
        [Authorize(Roles = $"{RoleConst.Admin},{RoleConst.User}")]
        [ProducesResponseType(typeof(BoardResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> MoveBoard(MoveBoardRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var res = await _service.MoveBoardAsync(request, long.Parse(userId));
            return Ok(res);
        }

    }
}
