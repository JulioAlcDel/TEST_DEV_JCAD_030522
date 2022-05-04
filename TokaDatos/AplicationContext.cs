using Microsoft.EntityFrameworkCore;
using TokaEntidad;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata;
namespace TokaDatos
{
    public class AplicationContext: DbContext
    {
        public readonly SqlParameter[] _param;
        public AplicationContext(DbContextOptions<AplicationContext> options, SqlParameter[] param) : base(options)
        {
            _param = param;
        }
        public virtual DbSet<PersonaFisica> Tb_PersonaFisicas { get; set; }
        public virtual DbSet<Usuario> Tb_Usuario { get; set; }
        public virtual DbSet<RespuestaSpPersonaFisica> SpPersonFisic { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonaFisica>(entity =>
            {
                entity.HasKey(e => e.IdPersonaFisica);
                entity.ToTable("Tb_PersonasFisicas");
                entity.Property(e => e.IdPersonaFisica).HasColumnName("IdPersonaFisica");
                entity.Property(e => e.FechaRegistro).HasColumnName("FechaRegistro");
                entity.Property(e => e.FechaActualizacion).HasColumnName("FechaActualizacion");
                entity.Property(e => e.Nombre).HasColumnName("Nombre");
                entity.Property(e => e.ApellidoPaterno).HasColumnName("ApellidoPaterno");
                entity.Property(e => e.ApellidoMaterno).HasColumnName("ApellidoMaterno");
                entity.Property(e => e.RFC).HasColumnName("RFC");
                entity.Property(e => e.FechaNacimiento).HasColumnName("FechaNacimiento");
                entity.Property(e => e.UsuarioAgrega).HasColumnName("UsuarioAgrega");
                entity.Property(e => e.Activo).HasColumnName("Activo");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);
                entity.ToTable("Tb_Usuario");
                entity.Property(e => e.IdUsuario).HasColumnName("IdUsuario");
                entity.Property(e => e.Correo).HasColumnName("Correo");
                entity.Property(e => e.Clave).HasColumnName("Clave");
                entity.Property(e => e.Sal).HasColumnName("Sal");
                entity.Property(e => e.Token).HasColumnName("Token");
            });

            modelBuilder.Entity<RespuestaSpPersonaFisica>(entity =>
            {
                entity.HasKey(e => e.ERROR);
            });
          }
        /// <summary>  
        /// Agrega una Persona Fisica
        /// </summary>  
        /// <returns>Retorna codigo y mensaje</returns>  
        public async Task<RespuestaSpPersonaFisica> AddPersonFisic(PersonaFisica personaFisica)
        {

            var param = sqlParameter(personaFisica, 1);
            var respuestaSpPersonas = await this.SpPersonFisic.FromSqlRaw("dbo.sp_AgregarPersonaFisica @Nombre,@ApellidoPaterno,@ApellidoMaterno,@RFC,@FechaNacimiento,@UsuarioAgrega", parameters: param).ToListAsync();
            return respuestaSpPersonas[0] ;
        }
        /// <summary>  
        /// Actualiza una Persona Fisica
        /// </summary>  
        /// <returns>Retorna codigo y mensaje</returns>  
        public async Task<RespuestaSpPersonaFisica> UpdatePersonFisic(PersonaFisica personaFisica)
        {

            var param = sqlParameter(personaFisica, 2);
            var respuestaSpPersonas = await this.SpPersonFisic.FromSqlRaw("dbo.sp_ActualizarPersonaFisica @IdPersonaFisica,@Nombre,@ApellidoPaterno,@ApellidoMaterno,@RFC,@FechaNacimiento,@UsuarioAgrega ", parameters: param).ToListAsync();
            return respuestaSpPersonas[0];
        }
        /// <summary>  
        /// Elimina una Persona Fisica
        /// </summary>  
        /// <returns>Retorna codigo y mensaje</returns>  
        public async Task<RespuestaSpPersonaFisica> DeletePersonFisic(int IdPersonaFisica)
        {
            var respuestaSpPersonas = await this.SpPersonFisic.FromSqlRaw("dbo.sp_EliminarPersonaFisica @IdPersonaFisica ", parameters: new SqlParameter("@IdPersonaFisica", IdPersonaFisica)).ToListAsync(); ;
            return respuestaSpPersonas[0];
        }
        public SqlParameter[] sqlParameter(PersonaFisica personaFisica,int tipoConsulta)
        {
            
            var parameters = new List<SqlParameter>();
            if (tipoConsulta == 2)
            {
                parameters.Add(new SqlParameter("@IdPersonaFisica", personaFisica.IdPersonaFisica));
            }
            if(tipoConsulta ==1 || tipoConsulta == 2)
            {
                parameters.Add(new SqlParameter("@Nombre", personaFisica.Nombre));
                parameters.Add(new SqlParameter("@ApellidoPaterno", personaFisica.ApellidoPaterno));
                parameters.Add(new SqlParameter("@ApellidoMaterno", personaFisica.ApellidoMaterno));
                parameters.Add(new SqlParameter("@RFC", personaFisica.RFC));
                parameters.Add(new SqlParameter("@FechaNacimiento", personaFisica.FechaNacimiento)); 
                parameters.Add(new SqlParameter("@UsuarioAgrega", personaFisica.UsuarioAgrega));
            }

            return parameters.ToArray();

        }
    }
}
