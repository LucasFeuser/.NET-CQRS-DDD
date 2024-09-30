CREATE DATABASE hangfire;

CREATE TABLE IF NOT EXISTS public.enderecos (
    id_endereco SERIAL PRIMARY KEY NOT NULL,              
    cep VARCHAR(8) NOT NULL,           
    numero INT NOT NULL,               
    rua VARCHAR(100) NOT NULL,
    complemento VARCHAR(100),
    bairro VARCHAR(100) NOT NULL,
    cidade VARCHAR(100) NOT NULL,
    uf VARCHAR(2) NOT NULL
);

CREATE TABLE IF NOT EXISTS public.pacientes (
    id_paciente SERIAL PRIMARY KEY NOT NULL,  
    num_cpf VARCHAR(11) NOT NULL,          
    nome_completo VARCHAR(255) NOT NULL,   
    data_nascimento TIMESTAMP NOT NULL,        
    email VARCHAR(255) NOT NULL,           
    genero CHAR(1),                         
    num_telefone VARCHAR(11),              
    plano_saude INTEGER,                   
    num_carteirinha VARCHAR(255),           
    notificacao_wpp BOOLEAN,
    endereco_id BIGINT NOT NULL,
    CONSTRAINT fk_paciente_endereco FOREIGN KEY (endereco_id) REFERENCES enderecos(id_endereco)                         
);

CREATE INDEX IF NOT EXISTS idx_paciente_cpf ON public.pacientes(num_cpf);



