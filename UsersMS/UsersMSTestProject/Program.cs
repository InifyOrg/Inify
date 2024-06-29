using UsersMS.Client;
using UsersMS.Contracts;

UsersMsClient client = new UsersMsClient();

UserDTO user = await client.GetUserByID(1);

Console.WriteLine($"{user.Id} = {user.Email}");