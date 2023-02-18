using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using tododotnet6prac.Dtos;
using tododotnet6prac.Models;
using tododotnet6prac.Parameters;

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
        //public IEnumerable<TodoListSelectDto> Get()
        //{
        //    var result = from a in _todoContext.TodoLists
        //                 where a.Name == "Item2"
        //                 select a;
        //    var result3 = _todoContext.TodoLists.ToList();
        //    var result2 = _todoContext.TodoLists.Where(a => a.Name == "walk dog");


        //    //try dto
        //    var result4 = _todoContext.TodoLists.Select(a => new TodoListSelectDto
        //    {
        //        TodoId = a.TodoId,
        //        Name = a.Name,
        //        InsertTime= a.InsertTime,
        //        UpdateTime = a.UpdateTime,
        //        Enable = a.Enable,
        //        Orders= a.Orders,
        //        InsertEmployee= a.Name,
        //        UpdateEmployee= a.Name,
        //    });
        //    // try automapper
        //    var result5 = _todoContext.TodoLists;

        //    return _iMapper.Map<IEnumerable<TodoListSelectDto>>(result5);
        //}

        [HttpGet]
        public IEnumerable<TodoListSelectDto> Get([FromQuery]TodoSelectParameters value) //bool 可不輸入 //修改成TodoSelectParameter時要用于指定参数如何绑定到 ASP.NET (有時asp,net 預設可能是錯誤的)
            //[FromQuery]：表示該參數的值應該從請求的查詢字符串中獲取。這通常用於獲取 GET 請求的參數。

            //[FromBody]：表示該參數的值應該從請求的正文中獲取。這通常用於獲取 POST、PUT 或 DELETE 請求的數據。在這種情況下，請求主體必須是可反序列化為參數類型的格式，例如 JSON 或 XML。

            //[FromHeader]：表示該參數的值應該從請求頭中獲取。例如，[FromHeader(Name = "Authorization")] 表示該參數的值應該從名為 "Authorization" 的請求頭中獲取。

            //[FromForm]：表示該參數的值應該從請求正文中的表單數據中獲取。這通常用於從 HTML 表單中獲取數據。

            //[FromRoute]：表示該參數的值應該從請求 URL 中的路由參數中獲取。例如，[FromRoute(Name = "id")] 表示該參數的值應該從名為 "id" 的路由參數中獲取。

            //[FromServices]：表示該參數的值應該從依賴注入服務中獲取。這通常用於從控制器外部獲取服務的實例。
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
                InsertTime = a.InsertTime,
                UpdateTime = a.UpdateTime,
                Enable = a.Enable,
                Orders = a.Orders,
                InsertEmployee = a.Name,
                UpdateEmployee = a.Name,
            });
            // try automapper
            var result5 = _todoContext.TodoLists;

            if(!string.IsNullOrWhiteSpace(value.name))
            {
                result4 = result4.Where(a => a.Name.Contains(value.name));
            }

            if(value.enable != null) 
            {
                result4 = result4.Where(a=>a.Enable == value.enable);
            }

            if(value.UpdateTime != null)
            {
                result4 = result4.Where(a=>a.UpdateTime.Date ==value.UpdateTime);
            }

            if (value.minOrder != null && value.maxOrder != null)
            {
                result4 = result4.Where(a => a.Orders>value.minOrder && a.Orders < value.maxOrder);
            }



            return result4;
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
