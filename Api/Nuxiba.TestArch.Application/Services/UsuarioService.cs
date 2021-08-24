using Nuxiba.TestArch.Domain.Repositories;
using Nuxiba.TestArch.Domain.Services;
using Nuxiba.TestArch.Domain.Services.Common;
using Nuxiba.TestArch.Domain.UnitOfWork;
using Nuxiba.TestArch.Entities;

namespace Nuxiba.TestArch.Application.Services
{
    public class UsuarioService : EntityService<Usuario>, IUsuarioService
    {
        IUnitOfWork _unitOfWork;
        IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUnitOfWork unitOfWork, IUsuarioRepository usuarioRepository)
            : base(unitOfWork, usuarioRepository)
        {
            _unitOfWork = unitOfWork;
            _usuarioRepository = usuarioRepository;
        }

        public Usuario GetById(int id)
        {
            return _usuarioRepository.GetById(id);
        }

        public Usuario GetByCorreoElectronico(string correoElectronico)
        {
            return _usuarioRepository.GetByCorreoElectronico(correoElectronico);
        }

        public Usuario GetByUsernameId(string username, int? id = null)
        {
            return _usuarioRepository.GetByUsernameId(username, id);
        }
    }
}