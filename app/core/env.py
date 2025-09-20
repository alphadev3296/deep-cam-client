from pydantic_settings import BaseSettings


class Config(BaseSettings):
    SERVER_URL: str = "http://localhost:8000/offer"


config = Config()
