from scipy.optimize import curve_fit
import math
import numpy as np

class approximation:
    def __init__(self):
        pass

    def run(self, x, y, bg, top_x, top_y, b, w):
        def mapping(vx, n):
            return bg + top_y * (n * (np.exp(-math.pi * (vx - top_x) ** 2) / b) + (1 - n) * (
                        w ** 2 / (w ** 2 + (vx - top_x) ** 2)))

        args, covar = curve_fit(mapping, np.array(x), np.array(y), bounds=(0, 1))

        return args[0]
