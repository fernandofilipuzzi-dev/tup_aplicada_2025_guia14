using GeometriaRestAPIWeb.DTOs;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL;

namespace GeometriaClientRestAPIDesktop;

public partial class FormPrincipal : Form
{
    private readonly GraphQLHttpClient _client;

    public FormPrincipal()
    {
        InitializeComponent();

        _client = new GraphQLHttpClient("https://localhost:7257/graphql", new NewtonsoftJsonSerializer());
    }

    async private void btnConsulta_Click(object sender, EventArgs e)
    {
        try
        {
            var request = new GraphQLRequest
            {
                Query = @"
 query {
      figuras {
 id
      tipo
      area
  ancho
          largo
                 radio
          }
        }"
            };

            var response = await _client.SendQueryAsync<FigurasResponse>(request);

            listBox1.Items.Clear();
            if (response.Data?.Figuras != null)
            {
                listBox1.DataSource = response.Data.Figuras;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}

public class FigurasResponse
{
    public List<FiguraDTO> Figuras { get; set; }
}
