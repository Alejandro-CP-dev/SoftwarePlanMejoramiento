use dbSoftwareMejoramiento;
go

-- ── Ubicación ──
create table Region (
    Id      int identity(1,1) primary key,
    Nombre  varchar(80)
);

create table Departamento (
    Id        int identity(1,1) primary key,
    Nombre    varchar(80),
    IdRegion  int,
    foreign key (IdRegion) references Region(Id)
);

create table Ciudad (
    Id             int identity(1,1) primary key,
    Nombre         varchar(80),
    IdDepartamento int,
    foreign key (IdDepartamento) references Departamento(Id)
);

-- ── Catálogos ──
create table EstadoAcademico (
    Id          int identity(1,1) primary key,
    Nombre      varchar(50),
    Descripcion varchar(255)
);

create table TipoPlan (
    Id          int identity(1,1) primary key,
    Nombre      varchar(50),
    Descripcion varchar(255)
);

create table Jornada (
    Id     int identity(1,1) primary key,
    Nombre varchar(50)
);

create table NivelFormacion (
    Id     int identity(1,1) primary key,
    Nombre varchar(50)
);

create table TipoArchivo (
    Id        int identity(1,1) primary key,
    Nombre    varchar(50),
    Extension varchar(10)
);

create table Especialidad (
    Id     int identity(1,1) primary key,
    Nombre varchar(100)
);

-- ── Académico ──
create table CentroFormacion (
    Id       int identity(1,1) primary key,
    Nombre   varchar(120),
    IdCiudad int,
    foreign key (IdCiudad) references Ciudad(Id)
);

create table Programa (
    Id       int identity(1,1) primary key,
    Codigo   varchar(50),
    Nombre   varchar(255),
    Version  varchar(50),
    Duracion int,
    Estado   varchar(50),
    IdNivel  int,
    foreign key (IdNivel) references NivelFormacion(Id)
);

create table CentroPrograma (
    Id         int identity(1,1) primary key,
    IdCentro   int,
    IdPrograma int,
    foreign key (IdCentro)   references CentroFormacion(Id),
    foreign key (IdPrograma) references Programa(Id)
);

create table Competencia (
    Id          int identity(1,1) primary key,
    Codigo      varchar(50),
    Nombre      varchar(255),
    Descripcion varchar(255),
    IdPrograma  int,
    foreign key (IdPrograma) references Programa(Id)
);

create table ResultadoAprendizaje (
    Id            int identity(1,1) primary key,
    Codigo        varchar(50),
    Descripcion   varchar(255),
    IdCompetencia int,
    foreign key (IdCompetencia) references Competencia(Id)
);

create table Ficha (
    Id                int identity(1,1) primary key,
    CodigoFicha       varchar(20) not null unique,
    FechaInicio       date,
    FechaFinalizacion date,
    Descripcion       varchar(255),
    Estado            varchar(50),
    IdPrograma        int,
    IdJornada         int,
    foreign key (IdPrograma) references Programa(Id),
    foreign key (IdJornada)  references Jornada(Id)
);

-- ── Personas ──
create table Administrador (
    Id              int identity(1,1) primary key,
    TipoDocumento   varchar(50),
    NumeroDocumento varchar(50),
    Nombre         varchar(120),
    Apellido       varchar(120),
    Correo          varchar(255),
    Telefono        varchar(50),
    Contrasena      varchar(255),
    Estado          varchar(50),
    IdCentro        int,
    foreign key (IdCentro) references CentroFormacion(Id)
);

create table Instructor (
    Id              int identity(1,1) primary key,
    TipoDocumento   varchar(50),
    NumeroDocumento varchar(50),
    Nombre          varchar(120),
    Apellido        varchar(120),
    Correo          varchar(255),
    Telefono        varchar(50),
    Contrasena      varchar(255),
    Estado          varchar(50),
    IdCentro        int,
    IdEspecialidad  int,
    foreign key (IdCentro) references CentroFormacion(Id)
);

create table Aprendiz (
    Id                int identity(1,1) primary key,
    TipoDocumento     varchar(50),
    NumeroDocumento   varchar(50),
    Nombre            varchar(120),
    Apellido          varchar(120),
    Correo            varchar(255),
    Telefono          varchar(50),
    Contrasena        varchar(255),
    Estado            varchar(50),
    IdEstadoAcademico int,
    foreign key (IdEstadoAcademico) references EstadoAcademico(Id)
);

-- ── Asignaciones ──
create table FichaInstructor (
    Id           int identity(1,1) primary key,
    IdFicha      int,
    IdInstructor int,
    foreign key (IdFicha)      references Ficha(Id),
    foreign key (IdInstructor) references Instructor(Id)
);

create table FichaAprendiz (
    Id         int identity(1,1) primary key,
    IdFicha    int,
    IdAprendiz int,
    foreign key (IdFicha)    references Ficha(Id),
    foreign key (IdAprendiz) references Aprendiz(Id)
);

-- ── Planes ──
create table PlanMejoramiento (
    Id              int identity(1,1) primary key,
    FechaAsignacion date,
    FechaLimite     date,
    Observaciones   varchar(255),
    EstadoPlan      varchar(50),
    IdAprendiz      int,
    IdInstructor    int,
    IdTipoPlan      int,
    foreign key (IdAprendiz)   references Aprendiz(Id),
    foreign key (IdInstructor) references Instructor(Id),
    foreign key (IdTipoPlan)   references TipoPlan(Id)
);

create table Actividad (
    Id                 int identity(1,1) primary key,
    Descripcion        varchar(255),
    FechaEntrega       date,
    Estado             varchar(50),
    IdPlanMejoramiento int,
    foreign key (IdPlanMejoramiento) references PlanMejoramiento(Id)
);

create table PlanResultado (
    Id                     int identity(1,1) primary key,
    IdPlanMejoramiento     int,
    IdResultadoAprendizaje int,
    foreign key (IdPlanMejoramiento)     references PlanMejoramiento(Id),
    foreign key (IdResultadoAprendizaje) references ResultadoAprendizaje(Id)
);

create table EvaluacionPlan (
    Id                     int identity(1,1) primary key,
    EvaluacionProducto     varchar(50),
    EvaluacionConocimiento varchar(50),
    EvaluacionDesempeno    varchar(50),
    ResultadoFinal         varchar(50),
    FechaEvaluacion        date,
    IdPlanMejoramiento     int,
    foreign key (IdPlanMejoramiento) references PlanMejoramiento(Id)
);

create table Evidencia (
    Id                 int identity(1,1) primary key,
    NombreArchivo      varchar(255),
    RutaArchivo        varchar(255),
    FechaCarga         date,
    Observaciones      varchar(255),
    IdPlanMejoramiento int,
    IdAprendiz         int,
    IdTipoArchivo      int,
    foreign key (IdPlanMejoramiento) references PlanMejoramiento(Id),
    foreign key (IdAprendiz)         references Aprendiz(Id),
    foreign key (IdTipoArchivo)      references TipoArchivo(Id)
);

create table InstructorEspecialidad (
    Id             int identity(1,1) primary key,
    IdInstructor   int,
    IdEspecialidad int,
    foreign key (IdInstructor)   references Instructor(Id),
    foreign key (IdEspecialidad) references Especialidad(Id)
);