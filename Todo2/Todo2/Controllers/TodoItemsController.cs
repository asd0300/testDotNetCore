using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Todo2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _todoContext;
        public TodoItemsController(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }
        // GET: api/<TodoItemsController>
        [HttpGet]
        public ActionResult< IEnumerable<TodoItem>> Get()
        {
            return _todoContext.TodoItems.ToList();
        }

        // GET api/<TodoItemsController>/5
        [HttpGet("{id}")]
        public ActionResult<TodoItem> Get(Guid id)
        {
            var result = _todoContext.TodoItems.Find(id);
            if(result == null)
            {
                return NotFound("找不到資源"); //respon 400
            }
            return result;
        }

        // POST api/<TodoItemsController>
        [HttpPost]
        public ActionResult<TodoItem> Post([FromBody] TodoItem value)
        {
            _todoContext.TodoItems.Add(value);
            _todoContext.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = value.Id},value); //create a 201 respon, add a location tag to point the path of source
        }

        // PUT api/<TodoItemsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] TodoItem value)
        {
            if(id!=value.Id)
            {
                return BadRequest();
            }
            _todoContext.Entry(value).State = Microsoft.EntityFrameworkCore.EntityState.Modified; //獲取或設置對象狀態

            //State属性使用EntityState枚举中的一个值来设置或获取实体状态。EntityState枚举包括以下值：

            //EntityState.Added：指示实体对象是新建的。
            //EntityState.Modified：指示实体对象已经被修改。
            //EntityState.Deleted：指示实体对象已被标记为删除。
            //EntityState.Unchanged：指示实体对象在上下文中没有发生更改。
            //EntityState.Detached：指示实体对象不再受上下文的管理。

            try
            {
                _todoContext.SaveChanges();
            }
            catch(DbUpdateException)
            {
                if(!_todoContext.TodoItems.Any(x => x.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500, "存取發生錯誤");
                }
            }
            return NoContent(); //它返回一个HTTP 204 No Content响应。这个响应表示服务器已经成功处理了请求，但没有返回任何数据，因此客户端不需要更新其现有的页面内容。
        }

        // DELETE api/<TodoItemsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var delete = _todoContext.TodoItems.Find(id);
            if(delete ==null)
            {
                return NotFound();
            }
            _todoContext.TodoItems.Remove(delete);
            _todoContext.SaveChanges();
            return NoContent();
        }
    }
}
