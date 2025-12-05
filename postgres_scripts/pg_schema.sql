CREATE EXTENSION IF NOT EXISTS vector;


CREATE TABLE items {
	id SERIAL PRIMARY KEY,
	name VARCHAR(255) NOT NULL,
	item_data JSONB,
	embedding vector(1536)
}