using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using tododotnet6prac.Dtos;
using tododotnet6prac.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace tododotnet6prac.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _todoContext;
        //加速建構子for DI
        private readonly IMapper _iMapper; // automapper 使用 
        public TodoController(TodoContext todoContext,IMapper iMapper)
        {
            _todoContext = todoContext;
            _iMapper = iMapper;
        }
        // GET: api/<TodoController>
        [HttpGet]
        public IEnumerable<TodoListSelectDto> Get()
        {
            var result = from a in _todoContext.TodoLists
                         where a.Name == "Item2"
                         select a;
            var result3 = _todoContext.TodoLists.ToList();
            var result2 = _todoContext.TodoLists.Where(a => a.Name == "walk dog");


            //try dto
            var result4 = _todoContext.TodoLists.Select(a => new TodoListSelectDto
            {
                TodoId = a.TodoId,
                Name = a.Name,
                InsertTime= a.InsertTime,
                UpdateTime = a.UpdateTime,
                Enable = a.Enable,
                Orders= a.Orders,
                InsertEmployee= a.Name,
                UpdateEmployee= a.Name,
            });
            // try automapper
            var result5 = _todoContext.TodoLists;

            return _iMapper.Map<IEnumerable<TodoListSelectDto>>(result5);
        }

        // GET api/<TodoController>/5
        [HttpGet("{id}")]
        public TodoListSelectDto Get(Guid id)
        {
            var result = (from a in _todoContext.TodoLists
                          join b in _todoContext.Employees on a.InsertEmployeeId equals b.EmployeeId
                          join c in _todoContext.Employees on a.UpdateEmployeeId equals c.EmployeeId
                         where a.TodoId == id
                          select new TodoListSelectDto
                          {
                              TodoId = a.TodoId,
                              Name = a.Name,
                              InsertTime = a.InsertTime,
                              UpdateTime = a.UpdateTime,
                              Enable = a.Enable,
                              Orders = a.Orders,
                              InsertEmployee = b.Name,
                              UpdateEmployee = c.Name,
                          }).SingleOrDefault(); //Single() 與SingleOrDefault() 差異 , 前者只回傳一筆資料如超過兩筆或null則抱錯，後者是有可能會有空資料集合
            return result;
        }

        // POST api/<TodoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TodoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TodoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
