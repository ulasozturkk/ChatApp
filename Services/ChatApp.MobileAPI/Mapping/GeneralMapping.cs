using AutoMapper;
using ChatApp.MobileAPI.Dtos;
using ChatApp.Models.Mobile;

namespace ChatApp.MobileAPI.Mapping {
  public class GeneralMapping : Profile {
    public GeneralMapping() {
      CreateMap<Message, MessageDTO>().ReverseMap();
      
    }
  }
}
