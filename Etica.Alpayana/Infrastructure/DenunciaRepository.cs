using Dapper;
using Etica.Alpayana.Domain.Entity;
using Etica.Alpayana.Domain.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Etica.Alpayana.Infrastructure
{
    public class DenunciaRepository : IDenunciaRepository
    {
        private readonly IDbConnection _db;
        public DenunciaRepository(IConfiguration config)
        {
            string connectionString = config.GetConnectionString("connection");
            _db = new SqlConnection(connectionString);
        }

        public async Task<List<Combo>> listarSede()
        {
            IEnumerable<Combo> lista = await _db.QueryAsync<Combo>("SELECT Tbespe Codigo, Tbdesc Descripcion FROM Base03 WHERE Tbiden = '333'");
          
            return lista.ToList();
        }
        public async Task<List<Combo>> listarTipoReporte()
        {
            IEnumerable<Combo> lista = await _db.QueryAsync<Combo>("SELECT Tbespe Codigo, Tbdesc Descripcion FROM Base03 WHERE Tbiden = '334'");
          
            return lista.ToList();
        }
        public int registrarDenuncia(Denuncia denuncia)
        {
            string query = "INSERT INTO Denuncia (TipoReporte_334, DenunciadoNombre, DenunciadoSede_333, " +
                            "DenunciadoPuesto, DenuncianteTipo, DenuncianteIdentidad, DenuncianteNombre, " +
                            "DenuncianteSede, DenuncianteCorreo, DenuncianteTelefono, VictimaNombre, " +
                            "VictimaSede, LugarIncidente, Detalle, Evidencia) " +
                            "VALUES (@TipoReporte, @DenunciadoNombre, @DenunciadoSede, " +
                            "@DenunciadoPuesto, @DenuncianteTipo, @DenuncianteIdentidad, @DenuncianteNombre, " +
                            "@DenuncianteSede, @DenuncianteCorreo, @DenuncianteTelefono, @VictimaNombre, " +
                            "@VictimaSede, @LugarIncidente, @Detalle, @Evidencia); " +
                            "SELECT SCOPE_IDENTITY()";

            using (var connection = new SqlConnection(_db.ConnectionString))
            {
                connection.Open();
                int idDenuncia = connection.QuerySingle<int>(query, new
                {
                    TipoReporte = denuncia.TipoReporte,
                    DenunciadoNombre = denuncia.DenunciadoNombre,
                    DenunciadoSede = denuncia.DenunciadoSede,
                    DenunciadoPuesto = denuncia.DenunciadoPuesto,
                    DenuncianteTipo = denuncia.DenuncianteTipo,
                    DenuncianteIdentidad = denuncia.DenuncianteIdentidad,
                    DenuncianteNombre = denuncia.DenuncianteNombre,
                    DenuncianteSede = denuncia.DenuncianteSede,
                    DenuncianteCorreo = denuncia.DenuncianteCorreo,
                    DenuncianteTelefono = denuncia.DenuncianteTelefono,
                    VictimaNombre = denuncia.VictimaNombre,
                    VictimaSede = denuncia.VictimaSede,
                    LugarIncidente = denuncia.LugarIncidente,
                    Detalle = denuncia.Detalle,
                    Evidencia = denuncia.Evidencia
                });
                connection.Close();
                return idDenuncia;
            }
        }

        public bool actualizarDenuncia(Denuncia denuncia)
        {
            bool actualizo = false;

            string query = "UPDATE Denuncia SET Evidencia = @Evidencia WHERE IdDenuncia = @IdDenuncia";

            using (var conn = new SqlConnection(_db.ConnectionString))
            {
                conn.Open();
                int rowsAffected = conn.Execute(query, new { Evidencia = denuncia.Evidencia, IdDenuncia = denuncia.IdDenuncia });
                if (rowsAffected > 0)
                    actualizo = true;
                conn.Close();
            }

            return actualizo;
        }


    }
}
