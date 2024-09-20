using System.Text.Json;
using static System.Console;

WriteLine("\nDigite o seu CEP: ");
var cep = ReadLine();

WriteLine("\nRealizando aquisição na API... ");
var enderecoUrl = $@"https://viacep.com.br/ws/{cep}/json/";

var cliente = new HttpClient();

try
{
  HttpResponseMessage? resposta = await cliente.GetAsync(enderecoUrl);
  resposta.EnsureSuccessStatusCode();

  string respostaApiString = await resposta.Content.ReadAsStringAsync();
  WriteLine("Imprimindo todo o json: " + respostaApiString);

  WriteLine("Transformando a minha string em um objeto C# :");
  // Deserializando a resposta JSON
  var objetoEndereco = JsonSerializer.Deserialize<Endereco>(respostaApiString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

  WriteLine("\nCEP: " + objetoEndereco.Cep);
  WriteLine("Rua: " + objetoEndereco.Logradouro);
  WriteLine("Estado: " + objetoEndereco.Estado);
}
catch (SystemException e)
{
  WriteLine("\nErro da Api: " + e.Message);
}