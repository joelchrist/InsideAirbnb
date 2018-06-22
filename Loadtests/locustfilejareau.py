from locust import HttpLocust, TaskSet, task

class InsideAirbnbUserBehavior(TaskSet):
    def on_start(self):
        pass

    @task(1)
    def get_all_summary_listings(self):
        self.client.get("/api/inside/summary-listings?review=&neighbourhood=&lowest-price=&highest-price=")
        
    @task(1)
    def get_all_summary_listings_with_filters(self):
        self.client.get("/api/inside/summary-listings?review=false&neighbourhood=De%20Pijp%20-%20Rivierenbuurt&lowest-price=100&highest-price=150")

    @task(1)
    def get_single_listing(self):
        self.client.get("/api/inside/listings/43109")
            
    @task(1)
    def get_all_listings_stats(self):
        self.client.get("/api/inside/listings/stats?review=&neighbourhood=&lowest-price=&highest-price=")
        
    @task(1)
    def get_all_listings_stats_with_filters(self):
        self.client.get("/api/inside/listings/stats?review=false&neighbourhood=De%20Pijp%20-%20Rivierenbuurt&lowest-price=100&highest-price=150")
        
class InsideAirbnbUser(HttpLocust):
    task_set = InsideAirbnbUserBehavior
    min_wait = 5000
    max_wait = 9000