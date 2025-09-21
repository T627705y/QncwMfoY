// 代码生成时间: 2025-09-21 18:33:40
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

// 定义RESTful API接口的服务类
[ApiController]
[Route("api/[controller]/[action]"]
public class ApiService : ControllerBase
{
    // 示例存储数据
    private static readonly List<string> items = new List<string>() { "item1", "item2", "item3" };

    // 获取所有项目的接口
    [HttpGet]
    public ActionResult<List<string>> GetAllItems()
    {
        try
        {
            return Ok(items);
        }
        catch (Exception ex)
        {
            // 错误处理
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    // 根据ID获取项目的接口
    [HttpGet]
    [Route("{id}")]
    public ActionResult<string> GetItemById(int id)
    {
        try
        {
            if (id >= items.Count || id < 0)
            {
                // 项目不存在
                return NotFound("Item not found.");
            }

            return Ok(items[id]);
        }
        catch (Exception ex)
        {
            // 错误处理
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    // 添加新项目的接口
    [HttpPost]
    public ActionResult<string> AddItem([FromBody] string item)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(item))
            {
                // 验证输入参数
                return BadRequest("Item cannot be null or empty.");
            }

            items.Add(item);
            return Ok(item);
        }
        catch (Exception ex)
        {
            // 错误处理
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    // 更新项目的接口
    [HttpPut]
    [Route("{id}")]
    public ActionResult<string> UpdateItem(int id, [FromBody] string item)
    {
        try
        {
            if (id >= items.Count || id < 0)
            {
                // 项目不存在
                return NotFound("Item not found.");
            }

            if (string.IsNullOrWhiteSpace(item))
            {
                // 验证输入参数
                return BadRequest("Item cannot be null or empty.");
            }

            items[id] = item;
            return Ok(item);
        }
        catch (Exception ex)
        {
            // 错误处理
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    // 删除项目的接口
    [HttpDelete]
    [Route("{id}")]
    public ActionResult<string> DeleteItem(int id)
    {
        try
        {
            if (id >= items.Count || id < 0)
            {
                // 项目不存在
                return NotFound("Item not found.");
            }

            items.RemoveAt(id);
            return Ok("Item deleted successfully.");
        }
        catch (Exception ex)
        {
            // 错误处理
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
