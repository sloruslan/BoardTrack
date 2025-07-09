using Application.DTO;
using Application.DTO.BoardType;
using Application.Interfaces.API;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/board-type")]
    public class BoardTypeController : BaseController<IBoardTypeService>
    {
        private readonly IBoardTypeService _service;
        public BoardTypeController(IBoardTypeService service) : base(service)
        {
            _service = service;
        }

        /// <summary>
        /// Добавить BoardType
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
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(BoardTypeResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateBoardTypeRequest request)
        {
            var res = await _service.CreateAsync(request);
            return Ok(res);
        }


        /// <summary>
        /// Получить BoardType
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
        [ProducesResponseType(typeof(List<BoardTypeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] FilteringBoardTypeRequest request)
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
        [ProducesResponseType(typeof(WithCountResponse<BoardTypeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWithCount([FromQuery] FilteringBoardTypeRequest request)
        {
            var res = await _service.GetWithCountAsync(request);
            return Ok(res);
        }

        /// <summary>
        /// Получить BoardType по id
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
        [ProducesResponseType(typeof(BoardTypeResponse), StatusCodes.Status200OK)]
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
        /// Удалить BoardType по id
        /// </summary>
        /// <param name="obj_id"></param>
        /// <response code="200">Ответ при успешном запросе</response>
        /// <response code="400">Невалидные входные данные</response>
        /// <response code="401">Не авторизован</response>
        /// <response code="403">доступ к запрашиваемому ресурсу запрещен</response>
        /// <response code="422">Необрабатываемый объект</response>
        /// <response code="500">Ошибка сервера</response>
        /// <response code="501">Не реализовано</response>
        [HttpDelete("{obj_id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteById(long obj_id)
        {
            await _service.DeleteAsync(obj_id);
            return Ok();
        }


        /// <summary>
        /// Обновить BoardType
        /// </summary>
        /// <param name="obj_id"></param>
        /// <param name="request"></param>
        /// <response code="200">Ответ при успешном запросе</response>
        /// <response code="400">Невалидные входные данные</response>
        /// <response code="401">Не авторизован</response>
        /// <response code="403">доступ к запрашиваемому ресурсу запрещен</response>
        /// <response code="422">Необрабатываемый объект</response>
        /// <response code="500">Ошибка сервера</response>
        /// <response code="501">Не реализовано</response>
        [HttpPut("{obj_id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(BoardTypeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateById(long obj_id, [FromBody] UpdateBoardTypeRequest request)
        {
            var res = await _service.UpdateAsync(obj_id, request);
            return Ok(res);
        }
    }
}
