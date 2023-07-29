using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace PeliculasApi.Helpers
{
    public class TypeBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var nombrePropiedad = bindingContext.ModelName; //necesitamos saber la propiedad con la que estamso trabajando
            var proveedorDeValores = bindingContext.ValueProvider.GetValue(nombrePropiedad); //nvesitamos aber el valor dedicha propiedad

            if (proveedorDeValores == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            //si no es nulo tenemos que desearizarlo
            try
            {
                //var valorDeserializado = JsonConvert
                //    .DeserializeObject<List<int>>(proveedorDeValores.FirstValue); //como recibiremos un array de enteros ponemos in list

                var valorDeserializado = JsonConvert
                    .DeserializeObject<T>(proveedorDeValores.FirstValue);

                bindingContext.Result = ModelBindingResult.Success(valorDeserializado);//valor desarelizaod sera el listado de ids que nos envia el clientede la app
            }
            catch
            {
                bindingContext.ModelState.TryAddModelError(nombrePropiedad, "Valor invalido para tipo LIt<int>");
            }

            return Task.CompletedTask;

        }
    }
}
