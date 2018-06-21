from locust import HttpLocust, TaskSet, task

class ApiUserBehaviour(TaskSet):
    def on_start(self):
        pass

    def on_stop(self):
        pass

    @task(1)
    def get_listings(self):
        self.client.get("/api/listings/filter")

    @task(1)
    def get_listings_only_jordaan(self):
        self.client.get("/api/listings/filter?neighbourhood=Jordaan")


    @task(1)
    def get_single_listing(self):
        self.client.get("/api/listings/16705542")


    @task(1)
    def get_price_history(self):
        self.client.get("/api/history/price")

class ApiUser(HttpLocust):

    """A user for the Airbnb web api"""

    task_set = ApiUserBehaviour
    min_wait = 5000
    max_wait = 9000
