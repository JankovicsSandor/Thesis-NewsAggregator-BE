FROM python:3.7-alpine as base

FROM base as builder


RUN mkdir /install
WORKDIR /install

COPY requirements.txt /requirements.txt


RUN pip install -r /requirements.txt --target .

FROM base
COPY --from=builder /install /usr/local/lib/python3.7/site-packages

WORKDIR /app
COPY . /app
CMD ["python", "api.py"]