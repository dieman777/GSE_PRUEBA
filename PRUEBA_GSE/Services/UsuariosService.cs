using MongoDB.Driver;
using PRUEBA_GSE.Controllers;
using PRUEBA_GSE.Models;
using System.Security.Cryptography;
using System.Text;

namespace PRUEBA_GSE.Services
{
    public class UsuariosService
    {
        //Diego: Importar del DTO de usuarios
        private readonly IMongoCollection<Usuarios> _usuarios;

        //Crear e inicializar el contexto
        public UsuariosService(MongoContextDB context) {
            _usuarios = context.GetCollection<Usuarios>("Usuarios");
        }

        //Obtener todos los usuarios
        public async Task<Usuarios?> GetUsuariosAsync(string usuario) {
            return await _usuarios.Find(u => u.usuario == usuario).FirstOrDefaultAsync();
        }

        public async Task<bool> ValidarLogin(string usuario, string contrasenia) {
            var listaUsuarios = await GetUsuariosAsync(usuario);


            if (listaUsuarios == null)
            { 
                return false;
            }

            return listaUsuarios.contrasenia == EncriptarContrasenia(contrasenia);
        }

        public async Task SetUsuarios(Usuarios usuarioN)
        {
            usuarioN.contrasenia = EncriptarContrasenia(usuarioN.contrasenia);
            await _usuarios.InsertOneAsync(usuarioN);
        }


        //Obtener el usuario por el Id
        public async Task<Usuarios?> GetUsauriosByIdAsync(string id)
        { 
            return await _usuarios.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        private string EncriptarContrasenia(string contrasenia)
        {
            using (SHA256 sh= SHA256.Create()) {
                var bytes = Encoding.UTF8.GetBytes(contrasenia);
                return Convert.ToBase64String(sh.ComputeHash(bytes));
            }
        }
    }
}
