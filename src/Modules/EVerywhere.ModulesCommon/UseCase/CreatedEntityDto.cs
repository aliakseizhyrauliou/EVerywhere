namespace EVerywhere.ModulesCommon.UseCase;

public class CreatedEntityDto<T>(T id)
{
    public T Id { get; set; } = id;
}