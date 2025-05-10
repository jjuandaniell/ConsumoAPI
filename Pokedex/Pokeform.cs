using Newtonsoft.Json;
using Pokedex.Clases;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokedex
{
    public partial class Pokeform : Form
    {
        public Pokeform()
        {
            InitializeComponent();
            ConfigurarColumnasDataGridView();
        }

        private void ConfigurarColumnasDataGridView()
        {
            dgvDatos.Columns.Clear();
            dgvDatos.AutoGenerateColumns = false;

            dgvDatos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nombre", DataPropertyName = "Nombre" });
            dgvDatos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Altura", DataPropertyName = "Altura" });
            dgvDatos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Peso", DataPropertyName = "Peso" });
            dgvDatos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tipos", DataPropertyName = "Tipos" });
            dgvDatos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Habilidades", DataPropertyName = "Habilidades" });
            dgvDatos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "HP", DataPropertyName = "HP" });
            dgvDatos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ataque", DataPropertyName = "Ataque" });
            dgvDatos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Defensa", DataPropertyName = "Defensa" });
            dgvDatos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Velocidad", DataPropertyName = "Velocidad" });
            dgvDatos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ataque Especial", DataPropertyName = "AtaqueEspecial" });
            dgvDatos.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Defensa Especial", DataPropertyName = "DefensaEspecial" });
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            string pokemonName = txtBusqueda.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(pokemonName))
            {
                MessageBox.Show("Por favor, ingresa el nombre de un Pokémon.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await BuscarPokemonAsync(pokemonName);
        }

        private async Task BuscarPokemonAsync(string pokemonName)
        {
            string apiUrl = $"https://pokeapi.co/api/v2/pokemon/{pokemonName}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string response = await client.GetStringAsync(apiUrl);
                    var pokemonData = JsonConvert.DeserializeObject<PokemonInfo>(response);

                    double alturaEnMetros = pokemonData.Height / 10.0;
                    double pesoEnKilogramos = pokemonData.Weight / 10.0;

                    string tipos = string.Join(", ", pokemonData.Types.Select(t => t.Type.Name));
                    string habilidades = string.Join(", ", pokemonData.Abilities.Select(a => a.Ability.Name));

                    var datos = new[]
                    {
                        new
                        {
                            Nombre = pokemonData.Name,
                            Altura = $"{alturaEnMetros} m",
                            Peso = $"{pesoEnKilogramos} kg",
                            Tipos = tipos,
                            Habilidades = habilidades,
                            HP = pokemonData.Stats.FirstOrDefault(s => s.Stat.Name == "hp")?.BaseStat ?? 0,
                            Ataque = pokemonData.Stats.FirstOrDefault(s => s.Stat.Name == "attack")?.BaseStat ?? 0,
                            Defensa = pokemonData.Stats.FirstOrDefault(s => s.Stat.Name == "defense")?.BaseStat ?? 0,
                            Velocidad = pokemonData.Stats.FirstOrDefault(s => s.Stat.Name == "speed")?.BaseStat ?? 0,
                            AtaqueEspecial = pokemonData.Stats.FirstOrDefault(s => s.Stat.Name == "special-attack")?.BaseStat ?? 0,
                            DefensaEspecial = pokemonData.Stats.FirstOrDefault(s => s.Stat.Name == "special-defense")?.BaseStat ?? 0
                        }
                    };

                    dgvDatos.DataSource = datos;

                    if (!string.IsNullOrEmpty(pokemonData.Sprites.FrontDefault))
                    {
                        using (HttpClient imageClient = new HttpClient())
                        {
                            var imageBytes = await imageClient.GetByteArrayAsync(pokemonData.Sprites.FrontDefault);
                            using (var ms = new MemoryStream(imageBytes))
                            {
                                pictureBox1.Image = Image.FromStream(ms);
                            }
                        }
                    }
                    else
                    {
                        pictureBox1.Image = null;
                        MessageBox.Show("No se encontró una imagen para este Pokémon.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (HttpRequestException)
                {
                    MessageBox.Show("No se encontró el Pokémon. Verifica el nombre e inténtalo de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBusqueda.Clear();
            dgvDatos.DataSource = null;
            pictureBox1.Image = null;
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
        }

        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
    }
}
