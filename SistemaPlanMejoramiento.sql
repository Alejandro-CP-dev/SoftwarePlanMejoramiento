-- ============================================================
--  SISTEMA DE GESTIÓN DE PLANES DE MEJORAMIENTO ACADÉMICO
--  SENA - SQL Server Express
--  Generado automáticamente desde el MER
-- ============================================================

USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'PlanMejoramientoSENA')
    DROP DATABASE PlanMejoramientoSENA;
GO

CREATE DATABASE PlanMejoramientoSENA;
GO

USE PlanMejoramientoSENA;
GO

-- ============================================================
-- 1. USUARIO
-- ============================================================
CREATE TABLE USUARIO (
    id_usuario      INT IDENTITY(1,1) PRIMARY KEY,
    username        VARCHAR(60)  NOT NULL UNIQUE,
    password_hash   VARCHAR(256) NOT NULL,
    rol             VARCHAR(20)  NOT NULL
                        CONSTRAINT CK_USUARIO_rol
                        CHECK (rol IN ('Administrador','Instructor','Aprendiz')),
    estado          VARCHAR(10)  NOT NULL DEFAULT 'Activo'
                        CONSTRAINT CK_USUARIO_estado
                        CHECK (estado IN ('Activo','Inactivo'))
);
GO

-- ============================================================
-- 2. CENTRO
-- ============================================================
CREATE TABLE CENTRO (
    id_centro   INT IDENTITY(1,1) PRIMARY KEY,
    nombre      VARCHAR(120) NOT NULL,
    regional    VARCHAR(80)  NOT NULL,
    ciudad      VARCHAR(80)  NOT NULL
);
GO

-- ============================================================
-- 3. PROGRAMA
-- ============================================================
CREATE TABLE PROGRAMA (
    id_programa      INT IDENTITY(1,1) PRIMARY KEY,
    codigo_programa  VARCHAR(20)  NOT NULL UNIQUE,
    nombre           VARCHAR(150) NOT NULL,
    version          VARCHAR(10)  NOT NULL,
    nivel_formacion  VARCHAR(50)  NOT NULL,
    duracion         INT          NOT NULL,   -- en horas
    estado           VARCHAR(10)  NOT NULL DEFAULT 'Activo'
                         CONSTRAINT CK_PROGRAMA_estado
                         CHECK (estado IN ('Activo','Inactivo'))
);
GO

-- ============================================================
-- 4. CENTRO_PROGRAMA  (N:M entre CENTRO y PROGRAMA)
-- ============================================================
CREATE TABLE CENTRO_PROGRAMA (
    id_centro   INT NOT NULL,
    id_programa INT NOT NULL,
    CONSTRAINT PK_CENTRO_PROGRAMA PRIMARY KEY (id_centro, id_programa),
    CONSTRAINT FK_CP_CENTRO   FOREIGN KEY (id_centro)   REFERENCES CENTRO  (id_centro),
    CONSTRAINT FK_CP_PROGRAMA FOREIGN KEY (id_programa) REFERENCES PROGRAMA (id_programa)
);
GO

-- ============================================================
-- 5. COMPETENCIA
-- ============================================================
CREATE TABLE COMPETENCIA (
    id_competencia  INT IDENTITY(1,1) PRIMARY KEY,
    codigo          VARCHAR(20)  NOT NULL,
    nombre          VARCHAR(200) NOT NULL,
    id_programa     INT          NOT NULL,
    CONSTRAINT FK_COMP_PROGRAMA FOREIGN KEY (id_programa)
        REFERENCES PROGRAMA (id_programa)
);
GO

-- ============================================================
-- 6. RESULTADO_APRENDIZAJE
-- ============================================================
CREATE TABLE RESULTADO_APRENDIZAJE (
    id_resultado    INT IDENTITY(1,1) PRIMARY KEY,
    codigo          VARCHAR(20)  NOT NULL,
    descripcion     VARCHAR(500) NOT NULL,
    id_competencia  INT          NOT NULL,
    CONSTRAINT FK_RA_COMPETENCIA FOREIGN KEY (id_competencia)
        REFERENCES COMPETENCIA (id_competencia)
);
GO

-- ============================================================
-- 7. FICHA
-- ============================================================
CREATE TABLE FICHA (
    id_ficha            INT IDENTITY(1,1) PRIMARY KEY,
    codigo_ficha        VARCHAR(20)  NOT NULL UNIQUE,
    fecha_inicio        DATE         NOT NULL,
    fecha_finalizacion  DATE         NOT NULL,
    jornada             VARCHAR(20)  NOT NULL
                            CONSTRAINT CK_FICHA_jornada
                            CHECK (jornada IN ('Diurna','Nocturna','Mixta','Madrugada')),
    descripcion         VARCHAR(300) NULL,
    estado              VARCHAR(20)  NOT NULL DEFAULT 'En ejecucion'
                            CONSTRAINT CK_FICHA_estado
                            CHECK (estado IN ('En ejecucion','Terminada','Cancelada')),
    id_programa         INT          NOT NULL,
    id_centro           INT          NOT NULL,
    CONSTRAINT FK_FICHA_PROGRAMA FOREIGN KEY (id_programa) REFERENCES PROGRAMA (id_programa),
    CONSTRAINT FK_FICHA_CENTRO   FOREIGN KEY (id_centro)   REFERENCES CENTRO   (id_centro)
);
GO

-- ============================================================
-- 8. INSTRUCTOR
-- ============================================================
CREATE TABLE INSTRUCTOR (
    id_instructor   INT IDENTITY(1,1) PRIMARY KEY,
    tipo_documento  VARCHAR(10)  NOT NULL
                        CONSTRAINT CK_INST_tdoc
                        CHECK (tipo_documento IN ('CC','CE','PP','TI')),
    num_documento   VARCHAR(20)  NOT NULL UNIQUE,
    nombres         VARCHAR(80)  NOT NULL,
    apellidos       VARCHAR(80)  NOT NULL,
    correo          VARCHAR(120) NOT NULL UNIQUE,
    telefono        VARCHAR(20)  NULL,
    especialidad    VARCHAR(100) NULL,
    id_usuario      INT          NOT NULL UNIQUE,
    CONSTRAINT FK_INST_USUARIO FOREIGN KEY (id_usuario)
        REFERENCES USUARIO (id_usuario)
);
GO

-- ============================================================
-- 9. FICHA_INSTRUCTOR  (N:M entre FICHA e INSTRUCTOR)
-- ============================================================
CREATE TABLE FICHA_INSTRUCTOR (
    id_ficha        INT  NOT NULL,
    id_instructor   INT  NOT NULL,
    fecha_asignacion DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE),
    CONSTRAINT PK_FICHA_INSTRUCTOR PRIMARY KEY (id_ficha, id_instructor),
    CONSTRAINT FK_FI_FICHA      FOREIGN KEY (id_ficha)      REFERENCES FICHA      (id_ficha),
    CONSTRAINT FK_FI_INSTRUCTOR FOREIGN KEY (id_instructor) REFERENCES INSTRUCTOR (id_instructor)
);
GO

-- ============================================================
-- 10. APRENDIZ
-- ============================================================
CREATE TABLE APRENDIZ (
    id_aprendiz       INT IDENTITY(1,1) PRIMARY KEY,
    tipo_documento    VARCHAR(10)  NOT NULL
                          CONSTRAINT CK_APR_tdoc
                          CHECK (tipo_documento IN ('CC','CE','PP','TI')),
    num_documento     VARCHAR(20)  NOT NULL UNIQUE,
    nombres           VARCHAR(80)  NOT NULL,
    apellidos         VARCHAR(80)  NOT NULL,
    correo            VARCHAR(120) NOT NULL UNIQUE,
    telefono          VARCHAR(20)  NULL,
    estado_academico  VARCHAR(30)  NOT NULL DEFAULT 'En Formacion'
                          CONSTRAINT CK_APR_estado
                          CHECK (estado_academico IN (
                              'En Formacion','Aplazado','Desertado',
                              'Retiro Voluntario','Condicionado',
                              'Cancelado','Certificado'
                          )),
    id_ficha          INT          NOT NULL,
    id_usuario        INT          NOT NULL UNIQUE,
    CONSTRAINT FK_APR_FICHA   FOREIGN KEY (id_ficha)   REFERENCES FICHA   (id_ficha),
    CONSTRAINT FK_APR_USUARIO FOREIGN KEY (id_usuario) REFERENCES USUARIO (id_usuario)
);
GO

-- ============================================================
-- 11. PLAN_MEJORAMIENTO
--     id_plan_padre permite la relacion recursiva
--     interno -> comite
-- ============================================================
CREATE TABLE PLAN_MEJORAMIENTO (
    id_plan          INT IDENTITY(1,1) PRIMARY KEY,
    tipo             VARCHAR(10)  NOT NULL
                         CONSTRAINT CK_PLAN_tipo
                         CHECK (tipo IN ('Interno','Comite')),
    fecha_asignacion DATE         NOT NULL DEFAULT CAST(GETDATE() AS DATE),
    actividades      VARCHAR(MAX) NOT NULL,
    observaciones    VARCHAR(MAX) NULL,
    fecha_limite     DATE         NOT NULL,
    estado           VARCHAR(20)  NOT NULL DEFAULT 'Pendiente'
                         CONSTRAINT CK_PLAN_estado
                         CHECK (estado IN ('Pendiente','En Proceso','Aprobado','No Aprobado')),
    id_aprendiz      INT          NOT NULL,
    id_instructor    INT          NOT NULL,
    id_plan_padre    INT          NULL,   -- NULL = plan interno; FK = plan comite
    CONSTRAINT FK_PLAN_APRENDIZ   FOREIGN KEY (id_aprendiz)   REFERENCES APRENDIZ   (id_aprendiz),
    CONSTRAINT FK_PLAN_INSTRUCTOR FOREIGN KEY (id_instructor) REFERENCES INSTRUCTOR (id_instructor),
    CONSTRAINT FK_PLAN_PADRE      FOREIGN KEY (id_plan_padre) REFERENCES PLAN_MEJORAMIENTO (id_plan)
);
GO

-- ============================================================
-- 12. PLAN_RESULTADO  (N:M entre PLAN_MEJORAMIENTO y RESULTADO_APRENDIZAJE)
-- ============================================================
CREATE TABLE PLAN_RESULTADO (
    id_plan      INT NOT NULL,
    id_resultado INT NOT NULL,
    CONSTRAINT PK_PLAN_RESULTADO PRIMARY KEY (id_plan, id_resultado),
    CONSTRAINT FK_PR_PLAN      FOREIGN KEY (id_plan)      REFERENCES PLAN_MEJORAMIENTO   (id_plan),
    CONSTRAINT FK_PR_RESULTADO FOREIGN KEY (id_resultado) REFERENCES RESULTADO_APRENDIZAJE (id_resultado)
);
GO

-- ============================================================
-- 13. EVALUACION
-- ============================================================
CREATE TABLE EVALUACION (
    id_evaluacion    INT IDENTITY(1,1) PRIMARY KEY,
    producto         VARCHAR(12) NOT NULL
                         CONSTRAINT CK_EVAL_producto
                         CHECK (producto IN ('Aprueba','No Aprueba')),
    conocimiento     VARCHAR(12) NOT NULL
                         CONSTRAINT CK_EVAL_conocimiento
                         CHECK (conocimiento IN ('Aprueba','No Aprueba')),
    desempeno        VARCHAR(12) NOT NULL
                         CONSTRAINT CK_EVAL_desempeno
                         CHECK (desempeno IN ('Aprueba','No Aprueba')),
    observaciones    VARCHAR(MAX) NULL,
    fecha_evaluacion DATE         NOT NULL DEFAULT CAST(GETDATE() AS DATE),
    id_plan          INT          NOT NULL UNIQUE,   -- 1 evaluacion por plan
    CONSTRAINT FK_EVAL_PLAN FOREIGN KEY (id_plan)
        REFERENCES PLAN_MEJORAMIENTO (id_plan)
);
GO

-- ============================================================
-- 14. EVIDENCIA
-- ============================================================
CREATE TABLE EVIDENCIA (
    id_evidencia           INT IDENTITY(1,1) PRIMARY KEY,
    nombre_archivo         VARCHAR(200) NOT NULL,
    tipo_archivo           VARCHAR(10)  NOT NULL
                               CONSTRAINT CK_EVID_tipo
                               CHECK (tipo_archivo IN ('PDF','DOCX','JPG','PNG','ZIP')),
    ruta_archivo           VARCHAR(500) NOT NULL,
    fecha_subida           DATETIME     NOT NULL DEFAULT GETDATE(),
    observacion_instructor VARCHAR(MAX) NULL,
    id_plan                INT          NOT NULL,
    CONSTRAINT FK_EVID_PLAN FOREIGN KEY (id_plan)
        REFERENCES PLAN_MEJORAMIENTO (id_plan)
);
GO

-- ============================================================
-- INDICES ADICIONALES (rendimiento en consultas frecuentes)
-- ============================================================
CREATE INDEX IX_APRENDIZ_ficha       ON APRENDIZ          (id_ficha);
CREATE INDEX IX_APRENDIZ_estado      ON APRENDIZ          (estado_academico);
CREATE INDEX IX_PLAN_aprendiz        ON PLAN_MEJORAMIENTO (id_aprendiz);
CREATE INDEX IX_PLAN_instructor      ON PLAN_MEJORAMIENTO (id_instructor);
CREATE INDEX IX_PLAN_estado          ON PLAN_MEJORAMIENTO (estado);
CREATE INDEX IX_EVIDENCIA_plan       ON EVIDENCIA         (id_plan);
CREATE INDEX IX_FICHA_INST_instructor ON FICHA_INSTRUCTOR (id_instructor);
GO

-- ============================================================
-- DATOS DE PRUEBA
-- ============================================================

-- Usuarios
INSERT INTO USUARIO (username, password_hash, rol) VALUES
('admin.centro',   'hash_admin_001',      'Administrador'),
('jperez.inst',    'hash_instructor_001', 'Instructor'),
('lgomez.inst',    'hash_instructor_002', 'Instructor'),
('carlos.apr',     'hash_aprendiz_001',   'Aprendiz'),
('maria.apr',      'hash_aprendiz_002',   'Aprendiz'),
('pedro.apr',      'hash_aprendiz_003',   'Aprendiz');
GO

-- Centro
INSERT INTO CENTRO (nombre, regional, ciudad) VALUES
('Centro de Comercio y Turismo', 'Regional Boyacá', 'Duitama');
GO

-- Programas
INSERT INTO PROGRAMA (codigo_programa, nombre, version, nivel_formacion, duracion, estado) VALUES
('228185', 'Análisis y Desarrollo de Software',   'v1', 'Tecnólogo', 2200, 'Activo'),
('122121', 'Contabilización de Operaciones Comerciales', 'v1', 'Técnico', 1400, 'Activo');
GO

-- Asociar programa al centro
INSERT INTO CENTRO_PROGRAMA (id_centro, id_programa) VALUES (1, 1), (1, 2);
GO

-- Competencias
INSERT INTO COMPETENCIA (codigo, nombre, id_programa) VALUES
('220501096', 'Construir software con tecnologías web',        1),
('220501097', 'Implementar bases de datos',                    1),
('110301010', 'Registrar operaciones contables',               2);
GO

-- Resultados de aprendizaje
INSERT INTO RESULTADO_APRENDIZAJE (codigo, descripcion, id_competencia) VALUES
('RA01', 'Aplicar el modelo orientado a objetos según los requerimientos del cliente', 1),
('RA02', 'Desarrollar interfaces de usuario cumpliendo los estándares de usabilidad',  1),
('RA03', 'Crear scripts DDL y DML según el modelo de base de datos',                   2),
('RA04', 'Implementar procedimientos almacenados y triggers',                          2),
('RA05', 'Registrar asientos contables básicos aplicando el PUC',                     3);
GO

-- Fichas
INSERT INTO FICHA (codigo_ficha, fecha_inicio, fecha_finalizacion, jornada, descripcion, estado, id_programa, id_centro) VALUES
('2887465', '2024-01-15', '2025-12-15', 'Diurna',  'Ficha ADSO grupo A', 'En ejecucion', 1, 1),
('2887466', '2024-02-01', '2025-06-30', 'Nocturna', 'Ficha Contabilidad A', 'En ejecucion', 2, 1);
GO

-- Instructores
INSERT INTO INSTRUCTOR (tipo_documento, num_documento, nombres, apellidos, correo, telefono, especialidad, id_usuario) VALUES
('CC', '12345678', 'Juan',  'Pérez Ruiz',   'jperez@sena.edu.co',  '3001234567', 'Ingeniería de Software', 2),
('CC', '87654321', 'Laura', 'Gómez Torres', 'lgomez@sena.edu.co',  '3107654321', 'Contabilidad y Finanzas', 3);
GO

-- Asignar instructores a fichas
INSERT INTO FICHA_INSTRUCTOR (id_ficha, id_instructor, fecha_asignacion) VALUES
(1, 1, '2024-01-15'),
(2, 2, '2024-02-01');
GO

-- Aprendices
INSERT INTO APRENDIZ (tipo_documento, num_documento, nombres, apellidos, correo, telefono, estado_academico, id_ficha, id_usuario) VALUES
('CC', '1001112223', 'Carlos',  'Ramírez León',   'carlos.ramirez@gmail.com',   '3121112223', 'En Formacion', 1, 4),
('CC', '1004445556', 'María',   'Suárez Moreno',  'maria.suarez@gmail.com',     '3134445556', 'En Formacion', 1, 5),
('TI', '1007778889', 'Pedro',   'López Cárdenas', 'pedro.lopez@gmail.com',      '3157778889', 'Condicionado', 1, 6);
GO

-- Planes de mejoramiento (internos)
INSERT INTO PLAN_MEJORAMIENTO (tipo, fecha_asignacion, actividades, observaciones, fecha_limite, estado, id_aprendiz, id_instructor, id_plan_padre) VALUES
(
    'Interno',
    '2024-09-10',
    'Desarrollar un CRUD completo usando POO en C#. Entregar código fuente y manual técnico.',
    'El aprendiz presentó dificultades en la aplicación del paradigma orientado a objetos.',
    '2024-09-25',
    'No Aprobado',
    3, 1, NULL
),
(
    'Interno',
    '2024-10-01',
    'Crear scripts DDL para una base de datos de inventario con mínimo 5 tablas relacionadas.',
    NULL,
    '2024-10-15',
    'Aprobado',
    1, 1, NULL
);
GO

-- Plan por comité (generado automáticamente del plan 1)
INSERT INTO PLAN_MEJORAMIENTO (tipo, fecha_asignacion, actividades, observaciones, fecha_limite, estado, id_aprendiz, id_instructor, id_plan_padre) VALUES
(
    'Comite',
    '2024-09-26',
    'Presentar ante el comité la corrección del ejercicio POO con sustentación oral.',
    'Generado automáticamente al no aprobar el plan interno N°1.',
    '2024-10-10',
    'Pendiente',
    3, 1, 1
);
GO

-- Resultados incumplidos asociados a los planes
INSERT INTO PLAN_RESULTADO (id_plan, id_resultado) VALUES
(1, 1),   -- Plan 1 -> RA01
(3, 1),   -- Plan comite -> mismo RA01
(2, 3);   -- Plan 2 -> RA03
GO

-- Evaluación del plan aprobado (plan 2)
INSERT INTO EVALUACION (producto, conocimiento, desempeno, observaciones, fecha_evaluacion, id_plan) VALUES
('Aprueba', 'Aprueba', 'Aprueba',
 'El aprendiz demostró dominio completo en DDL y relaciones entre tablas.',
 '2024-10-14', 2);
GO

-- Evidencias
INSERT INTO EVIDENCIA (nombre_archivo, tipo_archivo, ruta_archivo, fecha_subida, observacion_instructor, id_plan) VALUES
('solucion_crud_poo.zip',       'ZIP',  'evidencias/plan1/solucion_crud_poo.zip',        '2024-09-23 10:30:00', 'No implementó correctamente la herencia.',    1),
('script_inventario.sql',       'PDF',  'evidencias/plan2/script_inventario.pdf',         '2024-10-12 14:00:00', 'Excelente uso de FK y constraints.',           2),
('manual_tecnico_inventario.docx','DOCX','evidencias/plan2/manual_tecnico_inventario.docx','2024-10-12 14:05:00', NULL,                                          2),
('correccion_poo_comite.zip',   'ZIP',  'evidencias/plan3/correccion_poo_comite.zip',     '2024-10-08 09:00:00', NULL,                                          3);
GO

-- ============================================================
-- VISTAS ÚTILES
-- ============================================================

-- Vista: aprendices con sus planes activos
CREATE VIEW VW_APRENDICES_CON_PLANES AS
SELECT
    a.id_aprendiz,
    a.nombres + ' ' + a.apellidos        AS aprendiz,
    a.num_documento,
    a.estado_academico,
    f.codigo_ficha,
    p.id_plan,
    p.tipo                               AS tipo_plan,
    p.estado                             AS estado_plan,
    p.fecha_limite,
    i.nombres + ' ' + i.apellidos        AS instructor
FROM APRENDIZ a
INNER JOIN FICHA f                  ON a.id_ficha      = f.id_ficha
INNER JOIN PLAN_MEJORAMIENTO p      ON a.id_aprendiz   = p.id_aprendiz
INNER JOIN INSTRUCTOR i             ON p.id_instructor = i.id_instructor;
GO

-- Vista: resultados pendientes por aprendiz
CREATE VIEW VW_RESULTADOS_PENDIENTES AS
SELECT
    a.nombres + ' ' + a.apellidos        AS aprendiz,
    f.codigo_ficha,
    pm.id_plan,
    pm.tipo                              AS tipo_plan,
    ra.codigo                            AS cod_resultado,
    ra.descripcion                       AS resultado,
    c.nombre                             AS competencia,
    pm.fecha_limite
FROM APRENDIZ a
INNER JOIN PLAN_MEJORAMIENTO pm         ON a.id_aprendiz   = pm.id_aprendiz
INNER JOIN PLAN_RESULTADO pr            ON pm.id_plan       = pr.id_plan
INNER JOIN RESULTADO_APRENDIZAJE ra     ON pr.id_resultado  = ra.id_resultado
INNER JOIN COMPETENCIA c                ON ra.id_competencia= c.id_competencia
INNER JOIN FICHA f                      ON a.id_ficha       = f.id_ficha
WHERE pm.estado IN ('Pendiente','En Proceso');
GO

-- ============================================================
-- STORED PROCEDURES CLAVE
-- ============================================================

-- SP: Aprobar/rechazar evaluación y disparar lógica automática
CREATE PROCEDURE SP_EVALUAR_PLAN
    @id_plan        INT,
    @producto       VARCHAR(12),
    @conocimiento   VARCHAR(12),
    @desempeno      VARCHAR(12),
    @observaciones  VARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @aprueba        BIT = 0;
    DECLARE @id_aprendiz    INT;
    DECLARE @id_instructor  INT;
    DECLARE @nuevo_plan     INT;

    -- Registrar evaluación
    INSERT INTO EVALUACION (producto, conocimiento, desempeno, observaciones, fecha_evaluacion, id_plan)
    VALUES (@producto, @conocimiento, @desempeno, @observaciones, CAST(GETDATE() AS DATE), @id_plan);

    -- Determinar si aprueba todos los criterios
    IF @producto = 'Aprueba' AND @conocimiento = 'Aprueba' AND @desempeno = 'Aprueba'
        SET @aprueba = 1;

    IF @aprueba = 1
    BEGIN
        -- Aprobar el plan
        UPDATE PLAN_MEJORAMIENTO SET estado = 'Aprobado' WHERE id_plan = @id_plan;
    END
    ELSE
    BEGIN
        UPDATE PLAN_MEJORAMIENTO SET estado = 'No Aprobado' WHERE id_plan = @id_plan;

        -- Revisar tipo de plan para aplicar la regla automática
        SELECT @id_aprendiz = id_aprendiz, @id_instructor = id_instructor
        FROM PLAN_MEJORAMIENTO WHERE id_plan = @id_plan;

        DECLARE @tipo VARCHAR(10);
        SELECT @tipo = tipo FROM PLAN_MEJORAMIENTO WHERE id_plan = @id_plan;

        IF @tipo = 'Interno'
        BEGIN
            -- Generar plan por comité automáticamente
            INSERT INTO PLAN_MEJORAMIENTO
                (tipo, fecha_asignacion, actividades, observaciones, fecha_limite, estado, id_aprendiz, id_instructor, id_plan_padre)
            SELECT
                'Comite',
                CAST(GETDATE() AS DATE),
                actividades,
                'Generado automáticamente por no aprobar el plan interno N°' + CAST(@id_plan AS VARCHAR),
                DATEADD(DAY, 15, CAST(GETDATE() AS DATE)),
                'Pendiente',
                @id_aprendiz,
                @id_instructor,
                @id_plan
            FROM PLAN_MEJORAMIENTO WHERE id_plan = @id_plan;

            SET @nuevo_plan = SCOPE_IDENTITY();

            -- Copiar los resultados asociados al nuevo plan
            INSERT INTO PLAN_RESULTADO (id_plan, id_resultado)
            SELECT @nuevo_plan, id_resultado
            FROM PLAN_RESULTADO WHERE id_plan = @id_plan;
        END
        ELSE IF @tipo = 'Comite'
        BEGIN
            -- Cancelar al aprendiz automáticamente
            UPDATE APRENDIZ
            SET estado_academico = 'Cancelado'
            WHERE id_aprendiz = @id_aprendiz;
        END
    END
END;
GO

-- SP: Registrar evidencia
CREATE PROCEDURE SP_REGISTRAR_EVIDENCIA
    @id_plan        INT,
    @nombre_archivo VARCHAR(200),
    @tipo_archivo   VARCHAR(10),
    @ruta_archivo   VARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;

    IF @tipo_archivo NOT IN ('PDF','DOCX','JPG','PNG','ZIP')
    BEGIN
        RAISERROR('Tipo de archivo no permitido. Use PDF, DOCX, JPG, PNG o ZIP.', 16, 1);
        RETURN;
    END

    INSERT INTO EVIDENCIA (nombre_archivo, tipo_archivo, ruta_archivo, fecha_subida, id_plan)
    VALUES (@nombre_archivo, @tipo_archivo, @ruta_archivo, GETDATE(), @id_plan);

    UPDATE PLAN_MEJORAMIENTO
    SET estado = 'En Proceso'
    WHERE id_plan = @id_plan AND estado = 'Pendiente';
END;
GO

-- ============================================================
-- CONSULTAS DE VERIFICACIÓN
-- ============================================================

-- Todos los planes con su estado
SELECT
    pm.id_plan,
    a.nombres + ' ' + a.apellidos  AS aprendiz,
    pm.tipo,
    pm.estado,
    pm.fecha_limite,
    pm.id_plan_padre               AS generado_de_plan
FROM PLAN_MEJORAMIENTO pm
INNER JOIN APRENDIZ a ON pm.id_aprendiz = a.id_aprendiz
ORDER BY pm.id_plan;

-- Evidencias por plan
SELECT
    e.id_evidencia,
    pm.id_plan,
    pm.tipo,
    e.nombre_archivo,
    e.tipo_archivo,
    e.fecha_subida
FROM EVIDENCIA e
INNER JOIN PLAN_MEJORAMIENTO pm ON e.id_plan = pm.id_plan
ORDER BY e.fecha_subida;

-- Instructores y sus fichas
SELECT
    i.nombres + ' ' + i.apellidos AS instructor,
    f.codigo_ficha,
    p.nombre AS programa,
    fi.fecha_asignacion
FROM FICHA_INSTRUCTOR fi
INNER JOIN INSTRUCTOR i ON fi.id_instructor = i.id_instructor
INNER JOIN FICHA f      ON fi.id_ficha      = f.id_ficha
INNER JOIN PROGRAMA p   ON f.id_programa    = p.id_programa;
GO
