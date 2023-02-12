using AutoMapper;

namespace Trainer.Application.Mappings
{
    public interface IMapTo<T>
    {
        public void Mapping(Profile profile) => profile.CreateMap(GetType(), typeof(T));
    }
}
