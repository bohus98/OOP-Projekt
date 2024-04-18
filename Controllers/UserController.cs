using Microsoft.AspNetCore.Mvc;

namespace projekt.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    DataContextDapper _dapper;
   public UserController(IConfiguration config)
   {
        _dapper = new DataContextDapper(config);
   }

   [HttpGet("TestConnection")]
   public DateTime TestConnection()
   {
    return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
   }

    [HttpGet("GetUsers")]

    public IEnumerable<User> GetUsers()
    {
        string sql = @"SELECT [user_id],
                        [username],
                        [password],
                        [email],
                        [role] From [User]";
       IEnumerable<User> users = _dapper.LoadData<User>(sql);
       return users;
    }

        [HttpGet("GetUser/{user_id}")]

    public User GetUser(int user_id)
    {
        string sql = @"SELECT [user_id],
                        [username],
                        [password],
                        [email],
                        [role] From [User]
                            WHERE user_id= " + user_id.ToString();
       User user = _dapper.LoadDataSingle<User>(sql);
       return user;
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        string sql = @"UPDATE [User] SET [username] = '" + user.username +"',[password] = '" + user.password +"',[email] = '" + user.email +"',[role] = '" + user.role + "' WHERE [user_id] =" + user.user_id;

                    Console.WriteLine(sql);
        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to update user");
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser()
    {
        return Ok();
    }
}



